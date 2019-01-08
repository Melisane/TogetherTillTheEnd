using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMeleeAtk : MonoBehaviour {

    float timeActive = 0.2f;
    public bool affectsMage = false;

    void Update()
    {
        timeActive -= Time.deltaTime;
        if (timeActive <= 0)
            Destroy(gameObject);
    }
}
