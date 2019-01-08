using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour
{
    public int playerNum = 1;

    int currenthealth = 3;
    int maxHealth = 3;

    float alpha4 = 0.0f;
    float alpha5 = 0.0f;

    GameObject health1;
    GameObject health2;
    GameObject health3;
    GameObject health4;
    GameObject health5;

    protected Image image1;
    protected Image image2;
    protected Image image3;
    protected Image image4;
    protected Image image5;

    Warrior playerOne;
    Mage playerTwo;

    void Start ()
    {
        health1 = GameObject.Find("P" + playerNum + "-Health");
        health2 = GameObject.Find("P" + playerNum + "-Health2");
        health3 = GameObject.Find("P" + playerNum + "-Health3");
        health4 = GameObject.Find("P" + playerNum + "-Health4");
        health5 = GameObject.Find("P" + playerNum + "-Health5");

        if (playerNum == 1)
        {
            GameObject playerObject = GameObject.Find("Warrior");
            playerOne = playerObject.GetComponent<Warrior>();
        }
        else
        {
            GameObject playerObject = GameObject.Find("Mage");
            playerTwo = playerObject.GetComponent<Mage>();
        }

        image1 = health1.GetComponent<Image>();
        image2 = health2.GetComponent<Image>();
        image3 = health3.GetComponent<Image>();
        image4 = health4.GetComponent<Image>();
        image5 = health5.GetComponent<Image>();

    }
	
	
	void Update ()
    {
        if (playerNum == 1)
        {
            currenthealth = playerOne.health;
            maxHealth = playerOne.maxHealth;
        }
        else
        {
            currenthealth = playerTwo.health;
            maxHealth = playerTwo.maxHealth;
        }
            

        if(maxHealth == 3)
        {
            alpha4 = 0.0f;
            alpha5 = 0.0f;
        }
        if (maxHealth == 4)
        {
            alpha5 = 0.0f;
            if (currenthealth >= 4)
                alpha4 = 1.0f;
            else
                alpha4 = 0.5f;
            
        }
        if(maxHealth == 5 )
        {
            if (currenthealth >= 4)
                alpha4 = 1.0f;
            else
                alpha4 = 0.5f;

            if (currenthealth >= 5)
                alpha5 = 1.0f;
            else
                alpha5 = 0.5f;
        }


        switch (currenthealth)
        {
            case 5:
                image1.color = new Color(1f, 1f, 1f, 1f);
                image2.color = new Color(1f, 1f, 1f, 1f);
                image3.color = new Color(1f, 1f, 1f, 1f);
                image4.color = new Color(1f, 1f, 1f, alpha4);
                image5.color = new Color(1f, 1f, 1f, alpha5);
                break;
            case 4:
                image1.color = new Color(1f, 1f, 1f, 1f);
                image2.color = new Color(1f, 1f, 1f, 1f);
                image3.color = new Color(1f, 1f, 1f, 1f);
                image4.color = new Color(1f, 1f, 1f, alpha4);
                image5.color = new Color(0f, 0, 0, alpha5);
                break;
            case 3:
                image1.color = new Color(1f, 1f, 1f, 1f);
                image2.color = new Color(1f, 1f, 1f, 1f);
                image3.color = new Color(1f, 1f, 1f, 1f);
                image4.color = new Color(0f, 0, 0, alpha4);
                image5.color = new Color(0f, 0, 0, alpha5);
                break;
            case 2:
                image1.color = new Color(1f, 1f, 1f, 1f);
                image2.color = new Color(1f, 1f, 1f, 1f);
                image3.color = new Color(0f, 0, 0, 0.5f);
                image4.color = new Color(0f, 0, 0, alpha4);
                image5.color = new Color(0f, 0, 0, alpha5);
                break;
            case 1:
                image1.color = new Color(1f, 1f, 1f, 1f);
                image2.color = new Color(0f, 0, 0, 0.5f);
                image3.color = new Color(0f, 0, 0, 0.5f);
                image4.color = new Color(0f, 0, 0, alpha4);
                image5.color = new Color(0f, 0, 0, alpha5);
                break;
            case 0:
                image1.color = new Color(1f, 0, 0, 0.5f);
                image2.color = new Color(0f, 0, 0, 0.5f);
                image3.color = new Color(0f, 0, 0, 0.5f);
                image4.color = new Color(0f, 0, 0, alpha4);
                image5.color = new Color(0f, 0, 0, alpha5);
                break;
            default:
                break;
        }
	}
}
