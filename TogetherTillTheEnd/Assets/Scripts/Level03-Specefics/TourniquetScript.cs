using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourniquetScript : MonoBehaviour {

    [SerializeField]
    GameObject tourniquet, doorToNextCircle;
    [SerializeField]
    //public MonsterSpawner[] Spawners;
    static bool turn, collide;
    float initialRot;
    public float rotSpeed;
    public int numberOfTurns;

    //how many player are triggering the switch
    int nbrOfPlayerTriggering;

    //list of object that are colliding 
    //(to make sure both player influencing the same swith does not create irregularities) 
    List<GameObject> currentlyColliding;

    // Use this for initialization
    protected void Start () {
        turn = false;
        initialRot = 0;
        nbrOfPlayerTriggering = 0;
        currentlyColliding = new List<GameObject>();
    }

    // Update is called once per frame
      private void Update () {

            if (turn)
            {
                if (tourniquet.transform.rotation.z > (initialRot - 0.99))
                {
                    tourniquet.transform.rotation = new Quaternion(tourniquet.transform.rotation.x, tourniquet.transform.rotation.y, tourniquet.transform.rotation.z - Time.deltaTime * rotSpeed, tourniquet.transform.rotation.w);
                }

                else {

                    Debug.Log("IN ELSE");
                    tourniquet.transform.rotation = Quaternion.Euler(tourniquet.transform.rotation.x, tourniquet.transform.rotation.y, initialRot);


                            if (numberOfTurns > 0)
                            {

                                spawnMonster();
                                nbrOfPlayerTriggering = 0;
                                Debug.Log("Monster spawned");
                                numberOfTurns--;
                                turn = false;
                            }

                            else if (numberOfTurns == 0)
                            {

                                Destroy(tourniquet.gameObject);
                                Destroy(gameObject);
                                Debug.Log("OPEN DOOR");
                                Instantiate(doorToNextCircle, new Vector3(transform.position.x, transform.position.y - 2.3f, transform.position.z), Quaternion.identity);

                            }
                            
                }

            }


		
	    }


    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            nbrOfPlayerTriggering++;
            currentlyColliding.Add(collision.gameObject);
            Debug.Log("triggered nb of players = " + nbrOfPlayerTriggering);

            if (!turn)
            {
                if (nbrOfPlayerTriggering == 4)
                {
                    initialRot = tourniquet.transform.rotation.z;
                    turn = true;
                    Debug.Log("TURN TRUE : Initial Rot = " + initialRot + " transform rot = " + tourniquet.transform.rotation.z);
                }
            }
        }
        

    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            nbrOfPlayerTriggering--;
            currentlyColliding.Remove(collision.gameObject);
        }
        
    }

    private void spawnMonster() {

        /*for (int i = 0; i < Spawners.Length; i++)
        {
            Spawners[i].Trigger();
        }*/

    }



}
