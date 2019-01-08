using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactSwitch : MonoBehaviour
{
    public GameObject doorObject;
    Door door;
    bool activated = false;
    Vector3 startPosition;

	void Start ()
    {
        door = doorObject.GetComponent<Door>();
        startPosition = transform.position;
    }

	void Update ()
    {
		if(activated && transform.position.y > startPosition.y - 0.3f)
            transform.Translate(Vector3.down * Time.deltaTime * 4);
        else if(!activated && transform.position.y < startPosition.y)
            transform.Translate(Vector3.up * Time.deltaTime * 4);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PlayerOne" || col.gameObject.tag == "PlayerTwo")
        {
            activated = true;
            door.activated = true;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "PlayerOne" || col.gameObject.tag == "PlayerTwo")
        {
            activated = true;
            door.activated = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PlayerOne" || col.gameObject.tag == "PlayerTwo")
        {
            activated = false;
            door.activated = false;
        }
    }


}
