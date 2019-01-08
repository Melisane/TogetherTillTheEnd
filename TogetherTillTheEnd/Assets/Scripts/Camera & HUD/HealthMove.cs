using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthMove : MonoBehaviour
{
    GameObject health1;
    GameObject health2;
    GameObject health3;

    public int playerNum = 2;
    public int healthOverlay = 1;
    public bool goesDown = true;
    public bool beingRevived = false;

    protected Image image;

    public float ratio = 1.0f;

    void Start()
    {
        image = this.GetComponent<Image>();
    }
	
	void Update ()
    {
        if(goesDown && !beingRevived)
            ratio -= Time.deltaTime / 10.0f;
        else
            ratio += Time.deltaTime / 2.0f;
        transform.localScale = new Vector2(ratio, 1);
    }


    public void Reset(bool gDown, int pNum, int overlayNum)
    {
        playerNum = pNum;
        healthOverlay = overlayNum;
        goesDown = gDown;
        if (!goesDown)
        {
            ratio = 0.0f;
            image.color = new Color(0f, 1f, 0f, 1f);
        }
        else
            ratio = 1.0f;

        health1 = GameObject.Find("P" + playerNum + "-Health");
        health2 = GameObject.Find("P" + playerNum + "-Health2");
        health3 = GameObject.Find("P" + playerNum + "-Health3");

        switch (healthOverlay)
        {
            case 1:
                transform.position = new Vector3(health1.transform.position.x, health1.transform.position.y - 20, health1.transform.position.z);
                break;
            case 2:
                transform.position = new Vector3(health2.transform.position.x, health2.transform.position.y - 20, health2.transform.position.z);
                break;
            case 3:
                transform.position = new Vector3(health3.transform.position.x, health3.transform.position.y - 20, health3.transform.position.z);
                break;
            default:
                transform.position = new Vector3(health1.transform.position.x, health1.transform.position.y - 20, health1.transform.position.z);
                break;
        }
    }
}
