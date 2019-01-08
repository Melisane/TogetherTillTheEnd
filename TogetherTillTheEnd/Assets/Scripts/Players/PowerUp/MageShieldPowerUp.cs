using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageShieldPowerUp : MonoBehaviour {

    public Mage mage;

    void Start()
    {
        GameObject tempPlayer = GameObject.Find("Mage");
        mage = tempPlayer.GetComponent<Mage>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerTwo")
        {
            mage.hasShield = true;
            Destroy(gameObject);
        }

    }
}
