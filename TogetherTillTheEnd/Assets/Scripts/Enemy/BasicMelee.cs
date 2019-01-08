using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMelee : Monster
{
    UnityEngine.AI.NavMeshAgent navMeshAgent;

    float firstAttack = 1.0f;

    override protected void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
	
	override protected void Update()
    {
        base.Update();
        if(isAware && health > 0)   
        {
            //Moves toward Closest player, if awake and not in attack mode
            if ((mage.transform.position - transform.position).magnitude < (warrior.transform.position - transform.position).magnitude && !AttackMode)
            {
                if (mage.transform.position.x - transform.position.x > 0)
                {
                    spriteObject.FaceDirection(Vector2.right);
                    targetLocation = new Vector3(mage.transform.position.x - distanceFromTarget, mage.transform.position.y, mage.transform.position.z);
                    isLookingRight = true;
                }
                else
                {
                    spriteObject.FaceDirection(Vector2.left);
                    targetLocation = new Vector3(mage.transform.position.x + distanceFromTarget, mage.transform.position.y, mage.transform.position.z);
                    isLookingRight = false;
                }

                navMeshAgent.SetDestination(targetLocation);
            }
            else if(!AttackMode)
            {
                if (warrior.transform.position.x - transform.position.x > 0)
                {
                    spriteObject.FaceDirection(Vector2.right);
                    targetLocation = new Vector3(warrior.transform.position.x - distanceFromTarget, mage.transform.position.y, mage.transform.position.z);
                    isLookingRight = true;
                }
                else
                {
                    spriteObject.FaceDirection(Vector2.left);
                    targetLocation = new Vector3(warrior.transform.position.x + distanceFromTarget, mage.transform.position.y, mage.transform.position.z);
                    isLookingRight = false;
                }

                navMeshAgent.SetDestination(targetLocation);
            }

            if (((transform.position - warrior.transform.position).magnitude <= distanceFromTarget + 1.0f || (transform.position - mage.transform.position).magnitude <= distanceFromTarget + 1.0f) && navMeshAgent.velocity.x <= 0.5f) //Gestion Attaque
                AttackMode = true;
            else
                attackTimer = firstAttack;

        }
    }

    public override void Attack()
    {
        //Stuff()    Update Animator to attack
        attackTimer = attackTimer - Time.deltaTime;

        if (attackTimer <= 0.3f && doOnce)
        {
            doOnce = false;
            StartCoroutine(spriteObject.Flash());
        }

        if (attackTimer <= 0)
        {
            doOnce = true;
            AttackMode = false;
            attackTimer = attackSpeed;
            if (isLookingRight)
                Instantiate(EnnemyAtk, new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z), Quaternion.identity);
            else
                Instantiate(EnnemyAtk, new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z), Quaternion.identity * Quaternion.Euler(0, 0, 180));
        }
    }

    public override void UpdateAnimation()
    {
        doOnce = doOnce;
    }
}
