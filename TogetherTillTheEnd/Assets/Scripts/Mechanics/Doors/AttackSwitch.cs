using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSwitch : MonoBehaviour {

    public GameObject doorObject;
    Door door;
    bool activated = false;
    Vector3 startPosition;
    public string typeOfAttack = "MageRange";

    void Start()
    {
        door = doorObject.GetComponent<Door>();
        startPosition = transform.position;
    }

    void Update()
    {
        if (activated && transform.position.y > startPosition.y - 0.3f)
            transform.Translate(Vector3.down * Time.deltaTime * 4);
        else if (!activated && transform.position.y < startPosition.y)
            transform.Translate(Vector3.up * Time.deltaTime * 4);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == typeOfAttack)
        {
            activated = !activated;
            door.activated = !door.activated;
        }
    }

}
