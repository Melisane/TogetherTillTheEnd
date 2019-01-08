using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTeleportPowerUp : MonoBehaviour {

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
            playerTwo.hasTeleport = true;
            Destroy(gameObject);
        }

    }
}
