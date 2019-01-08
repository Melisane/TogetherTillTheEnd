using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallTeleport : MonoBehaviour
{
    GameObject playerOne;
    GameObject playerTwo;

    Warrior warrior;
    Mage mage;

    void Start ()
    {
        playerOne = GameObject.Find("Warrior");
        warrior = playerOne.GetComponent<Warrior>();
        playerTwo = GameObject.Find("Mage");
        mage = playerTwo.GetComponent<Mage>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PlayerOne")
        {
            playerTwo.transform.position = playerOne.transform.position;
            mage.rigidBody.velocity = warrior.rigidBody.velocity;
        }
        else if(col.gameObject.tag == "PlayerTwo")
        {
            playerOne.transform.position = playerTwo.transform.position;
            warrior.rigidBody.velocity = mage.rigidBody.velocity;
        }
    }
}
