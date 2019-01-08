using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorShield : MonoBehaviour {

    public Warrior Warrior;

    void Start()
    {
        GameObject tempPlayer = GameObject.Find("Warrior");
        Warrior = tempPlayer.GetComponent<Warrior>();
    }

    private void Update()
    {
        this.transform.position = Warrior.transform.position;
    }

}
