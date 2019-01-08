using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardProjectile : Hazard {

    public Vector2 ProjectileDir;

    [SerializeField]
    public float timeBeforeDespawn;
    [SerializeField]
    public float projectileSpeed;

    // Update is called once per frame
    void Update () {

        if (timeBeforeDespawn <= 0)
        {
            Destroy(gameObject);
        }

        transform.Translate(ProjectileDir * projectileSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Players"))
        {
            damagePLayer(collision.gameObject);
            Destroy(gameObject);

        }

        else
        {
            Destroy(gameObject);
        }

    }

    public void SetDirection(Vector3 aDirection)
    {
        ProjectileDir = aDirection;
    }

    public void SetSpeed(float aSpeed)
    {
        projectileSpeed = aSpeed;
    }
}
