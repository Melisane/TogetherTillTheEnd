using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMeleePowerUp : MonoBehaviour {

    public Mage playerTwo;

    void Start()
    {
        GameObject tempPlayer = GameObject.Find("Mage");
        playerTwo = tempPlayer.GetComponent<Mage>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerTwo")
        {
            playerTwo.hasMeleeAtk = true;
            Destroy(gameObject);
        }

    }
}
