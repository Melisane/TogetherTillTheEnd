using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyProjectile : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public bool physicalDmg = true;
    public int damage = 1;

    GameObject mage;
    GameObject warrior;

    Vector3 direction;


    void Start()
    {
        mage = GameObject.Find("Mage");
        warrior = GameObject.Find("Warrior");

        if ((mage.transform.position - transform.position).magnitude < (warrior.transform.position - transform.position).magnitude)
            direction = (mage.transform.position - transform.position).normalized;
        else
            direction = (warrior.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "PlayerOne")
        {
            Warrior war = warrior.GetComponent<Warrior>();
            war.TakeDamage(damage, physicalDmg);
        }
        if (col.gameObject.tag == "PlayerTwo")
        {
            Mage mag = mage.GetComponent<Mage>();
            mag.TakeDamage(damage, physicalDmg);
        }
        Destroy(gameObject);
    }

}
