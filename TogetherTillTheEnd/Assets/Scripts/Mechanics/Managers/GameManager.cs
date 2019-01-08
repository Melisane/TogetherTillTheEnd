using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Mage mage;
    Warrior warrior;

    int CHECKPOINT = 0;
    public GameObject checkpoint;

    public GameObject healthBarObject;
    HealthMove healthBar;
    public GameObject GameOver;

    bool doOnce = true;
    bool doOnce2 = true;

    void Start ()
    {
        CHECKPOINT = PlayerPrefs.GetInt("CheckPoint");

        GameOver.SetActive(false);
        GameObject playerMage = GameObject.Find("Mage");
        mage = playerMage.GetComponent<Mage>();

        GameObject playerWarrior = GameObject.Find("Warrior");
        warrior = playerWarrior.GetComponent<Warrior>();

        GameObject HUD = Instantiate(healthBarObject, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        HUD.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        healthBar = HUD.GetComponent<HealthMove>();
        healthBarObject = HUD;
        healthBarObject.SetActive(false);

        if (CHECKPOINT > 0)
        {
            warrior.transform.position = checkpoint.transform.position;
            mage.transform.position = checkpoint.transform.position + new Vector3(2, 0, 0);
        }
    }
	
	void Update ()
    {
		if(mage.health <= 0)
        {
            GestionDeathMage();
        }
        if(warrior.health <= 0)
        {
            GestionDeathWarrior();
        }

        if(warrior.health <= 0 && mage.health <= 0)
        {
            GameOver.SetActive(true);
        }
	}

    void GestionDeathMage()
    {
        if (doOnce)
        {
            doOnce = false;
            healthBarObject.SetActive(true);
            healthBar.Reset(true, 2, 1);
        }

        if (Input.GetButton("WarriorRevive") && Vector3.Magnitude(warrior.transform.position - mage.transform.position) <= 2.0f)
        {
            healthBar.beingRevived = true;
        }
        else
            healthBar.beingRevived = false;

        if (healthBar.ratio >= 1.1f)
        {
            doOnce = true;
            healthBarObject.SetActive(false);
            mage.health = 1;
            mage.isDead = false;
            mage.animator.SetTrigger("Revived");
        }

        if (healthBar.ratio <= 0)
        {
            if (doOnce2)
            {
                healthBarObject.SetActive(false);
                doOnce = false;
                GameOver.SetActive(true);
            }
        }
    }

    void GestionDeathWarrior()
    {
        if (doOnce)
        {
            doOnce = false;
            healthBarObject.SetActive(true);
            healthBar.Reset(true, 1, 1);
        }

        if (Input.GetButton("MageRevive") && Vector3.Magnitude(warrior.transform.position - mage.transform.position) <= 2.0f)
        {
            healthBar.beingRevived = true;
        }
        else
            healthBar.beingRevived = false;

        if (healthBar.ratio >= 1.1f)
        {
            doOnce = true;
            healthBarObject.SetActive(false);
            warrior.health = 1;
            warrior.isDead = false;
            warrior.animator.SetTrigger("Revived");
        }

        if (healthBar.ratio <= 0)
        {
            if (doOnce2)
            {
                healthBarObject.SetActive(false);
                doOnce = false;
                GameOver.SetActive(true);
            }
        }
    }
}
