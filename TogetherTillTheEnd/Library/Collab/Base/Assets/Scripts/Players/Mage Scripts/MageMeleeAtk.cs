using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMeleeAtk : MonoBehaviour
{
    float timeActive = 0.2f;
	
	
	void Update ()
    {
        timeActive -= Time.deltaTime;
        if (timeActive <= 0)
            Destroy(gameObject);
	}

    void OnCollisionEnter2D(Collision2D col)    //For now deletes on any hit
    {
        if (col.gameObject.tag == "Monster") //Check for monster or object
            Destroy(col.gameObject);
        else       //If nothing uselful, delete
            Destroy(gameObject);
    }
}
