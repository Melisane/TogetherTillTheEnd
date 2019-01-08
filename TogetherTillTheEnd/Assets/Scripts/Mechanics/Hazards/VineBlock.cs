using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineBlock : MonoBehaviour {

    public string tagOfAtk = "WarriorMelee";

    public GameObject block;
    Rigidbody2D blockBody;

    void Start()
    {
        blockBody = block.GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == tagOfAtk)
        {
            blockBody.gravityScale = 4;
            blockBody.constraints = RigidbodyConstraints2D.None;
            Destroy(gameObject);
        }
    }
}
