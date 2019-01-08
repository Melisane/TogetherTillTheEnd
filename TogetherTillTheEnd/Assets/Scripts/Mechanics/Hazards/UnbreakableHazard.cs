using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnbreakableHazard : Hazard {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            damagePLayer(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            damagePLayer(collision.gameObject);
        }
    }
}
