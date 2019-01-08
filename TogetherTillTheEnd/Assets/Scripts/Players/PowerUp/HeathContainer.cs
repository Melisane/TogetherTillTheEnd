using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathContainer : MonoBehaviour
{
    Mage mage;
    Warrior warrior;

    public GameObject pickUpEffect;

    void Start()
    {
        GameObject playerMage = GameObject.Find("Mage");
        mage = playerMage.GetComponent<Mage>();

        GameObject playerWarrior = GameObject.Find("Warrior");
        warrior = playerWarrior.GetComponent<Warrior>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "PlayerTwo" || col.gameObject.tag == "PlayerOne")
        {
            Instantiate(pickUpEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation); 

            mage.maxHealth++;
            mage.health = mage.maxHealth;
            warrior.maxHealth++;
            warrior.health = mage.maxHealth;
            Destroy(gameObject);
        }
    }
}
