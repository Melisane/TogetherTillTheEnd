using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableHazard : Hazard {

    //Should add to create a destroyed animation
    //[SerializeField]
    //GameObject destroyedAnimPrefab;

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
           damagePLayer(collision.gameObject);
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("RockForm"))
        {
            Destroy(gameObject);
            //Instantiate(destroyedAnimPrefab, transform.position);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("RockForm"))
        {
            Destroy(gameObject);
            //Instantiate(destroyedAnimPrefab, transform.position);
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
           damagePLayer(collision.gameObject);
        }
    }
}
