using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesScript :MonoBehaviour {

    //list of object that are colliding 
    //(to make sure both player influencing the same swith does not create irregularities) 
    List<GameObject> currentlyColliding;
    Collider2D thisCollider;
    //public AudioSource Do, Re, Mi, Fa;

    //how many player are triggering the switch
    public int nbrOfPlayerTriggering;
    static bool firstNote = false;
    bool moveup, movedown;
    public float speed;
    public float time;
    float timer;
    Vector3 initialPosition;


    // Use this for initialization
    protected void Start()
    {
        timer = time;
        moveup = false;
        movedown = false;
        nbrOfPlayerTriggering = 0;
        initialPosition = transform.position;
        currentlyColliding = new List<GameObject>();
        thisCollider = GetComponent<Collider2D>();
    }

    private void Update()
    {

        if (transform.position.y <= initialPosition.y && nbrOfPlayerTriggering == 0)
            transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);


        if (transform.position.y >= (initialPosition.y - 0.2f) && nbrOfPlayerTriggering > 0)
            transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);


        //if (thisCollider.name == "Fa")
          //  Debug.Log(nbrOfPlayerTriggering);

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            //SwitchDown
            nbrOfPlayerTriggering++;
            currentlyColliding.Add(collision.gameObject);

            if (thisCollider.name == "Do") {

                firstNote = false;
               // Do.Play();

            }

            else if (thisCollider.name == "Re")
            {
                firstNote = true;
               // Re.Play();
            }

            else if (thisCollider.name == "Mi")
            {
                firstNote = false;
              //  Mi.Play();
            }

            else if (thisCollider.name == "Fa" && firstNote)
            {
               // Fa.Play();
                //spawnShields.spawn = true;
            }

            else {

                firstNote = false;
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Players"))
            nbrOfPlayerTriggering--;
    }

}
