using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorShieldPowerUp : MonoBehaviour {


    public Warrior warrior;

    void Start()
    {
        GameObject tempPlayer = GameObject.Find("Warrior");
        warrior = tempPlayer.GetComponent<Warrior>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerOne")
        {
            warrior.hasShield = true;
            Destroy(gameObject);
        }

    }
}
