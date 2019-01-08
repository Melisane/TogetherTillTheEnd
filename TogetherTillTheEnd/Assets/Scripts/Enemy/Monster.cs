using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour {

    //References to Players and Atk
    protected GameObject mage;
    protected GameObject warrior;
    public GameObject EnnemyAtk;

    //Variables Per Monster
    public float rangeOfVision = 15.0f;
    public float distanceFromTarget = 1.0f;
    public float health = 3.0f;
    public float attackSpeed = 1.5f;
    public  float attackTimer = 1.5f;
    //Variables Per Monster Type
    public bool isPhysicalImuned = false;
    public float physicalResistance = 0.0f;
    public bool isMagicalImuned = false;
    public float magicalResistance = 0.0f;
    public Color phyRes;
    public Color phyImune;
    public Color magRes;
    public Color magImune;

    //Sprite
    public EnnemySprite spriteObject;

    //Utilities
    protected bool isAware = false;
    protected float invisibilityFrames = 0.0f;
    protected Vector3 targetLocation;
    protected bool isLookingRight = true;
    protected bool doOnce = true;
    protected bool AttackMode = false;
    protected float maxHealth;

    //Vision
    RaycastHit rayHit;
    Vector3 rayDirection;
    protected bool hasVisionOnMage = false;
    protected bool hasVisionOnWarrior = false;


    virtual protected void Start()
    {
        maxHealth = health;
        mage = GameObject.Find("Mage");
        warrior = GameObject.Find("Warrior");
        Tint();
    }

    virtual protected void Update()
    {
        UpdateAnimation();
        invisibilityFrames = -Time.deltaTime;

        rayDirection = mage.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, out rayHit))
        {
            if (rayHit.collider.tag == "PlayerTwo")
                hasVisionOnMage = true;
            else
                hasVisionOnMage = false;
        }

        rayDirection = warrior.transform.position - transform.position;
        if (Physics.Raycast(transform.position, rayDirection, out rayHit))
        {
            if (rayHit.collider.tag == "PlayerOne")
                hasVisionOnWarrior = true;
            else
                hasVisionOnWarrior = false;
        }


        if (health <= 0)     //Death
        {
            Destroy(gameObject);
            Destroy(spriteObject.gameObject);
        }

        

        //Checks to see if player is in range to awaken
        if ((mage.transform.position - transform.position).magnitude <= rangeOfVision || (warrior.transform.position - transform.position).magnitude <= rangeOfVision || health < maxHealth)
            isAware = true;

        

        if (isAware && health > 0)
        {
            if (AttackMode)
            {
                Attack();
            }
        }
    }

    public abstract void Attack();

    public abstract void UpdateAnimation();

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "MageRange")
        {
            if (!isMagicalImuned)
                TakeDamage(2 - magicalResistance);
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "WarriorRange")
        {
            if (!isPhysicalImuned)
                TakeDamage(1 - physicalResistance);
            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "MageMelee")
        {
            if (!isMagicalImuned)
                TakeDamage(2 - magicalResistance);
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "WarriorMelee")
        {
            if (!isPhysicalImuned)
                TakeDamage(3 - physicalResistance);
            Destroy(col.gameObject);
        }
    }

    void TakeDamage(float damage)
    {
        health -= damage;
    }

    void Tint()
    {
        if (physicalResistance > 0)
            spriteObject.setTint(phyRes);
        if (magicalResistance > 0)
            spriteObject.setTint(magRes);
        if (isPhysicalImuned)
            spriteObject.setTint(phyImune);
        if (isMagicalImuned)
            spriteObject.setTint(magImune);
    }
}
