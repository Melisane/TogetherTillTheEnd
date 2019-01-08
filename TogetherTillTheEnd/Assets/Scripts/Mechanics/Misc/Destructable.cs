using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    //Must check isTrigger for Melee

    public string tagOfAtk = "WarriorMelee";

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == tagOfAtk)
        {
            DestroyObject();
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == tagOfAtk)
        {
            DestroyObject();
        }
    }

    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag == tagOfAtk)
        {
            DestroyObject();
        }
    }

    void DestroyObject()    //If we want to add any effects, should be in here.
    {
        Destroy(gameObject);
    }

}
