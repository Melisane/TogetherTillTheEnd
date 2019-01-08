using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMeleeAtk : MonoBehaviour
{
    float timeActive = 0.2f;
    public bool affectsWarrior = false;

    void Update ()
    {
        timeActive -= Time.deltaTime;
        if (timeActive <= 0)
            Destroy(gameObject);
	}

    void OnCollisionEnter2D(Collision2D col)    //For now deletes on any hit
    {
        if (col.gameObject.tag == "PlayerOne" && affectsWarrior)
        {
            col.gameObject.GetComponent<Warrior>().TakeDamage(1, false);
        }
        if (col.gameObject.tag == "Monster") //Check for monster or object
            Destroy(col.gameObject);
        else       //If nothing useful, delete
            Destroy(gameObject);
    }
}
