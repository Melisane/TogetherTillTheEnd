using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageShield : MonoBehaviour
{

    public GameObject PlayerTwo;


    void Start ()
    {
        PlayerTwo = GameObject.Find("Mage");
    }
	
	void Update ()
    {
        this.transform.position = PlayerTwo.transform.position;
    }
}
