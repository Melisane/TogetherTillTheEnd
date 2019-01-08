using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pit hazards Should only be used when we expect the player to respawn out of the pit if he falls in.
public class PitHazard : Hazard {

    [SerializeField]
    Vector3 RespawnPosition;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players") || collision.gameObject.layer == LayerMask.NameToLayer("RockForm"))
        {
            damagePLayer(collision.gameObject);
            playerRespawn(collision.gameObject);
        }
    }

    void playerRespawn(GameObject aPlayer)
    {
        aPlayer.transform.position = RespawnPosition;
    }
}
