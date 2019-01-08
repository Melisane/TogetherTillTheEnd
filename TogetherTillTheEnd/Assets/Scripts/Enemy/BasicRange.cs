using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicRange : Monster {

    UnityEngine.AI.NavMeshAgent navMeshAgent;
    float iniDistanceFromTarget;
    public GameObject EnnemyRangeAtk;

    override protected void Start()
    {
        base.Start();
        iniDistanceFromTarget = distanceFromTarget;
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    override protected void Update()
    {
        base.Update();

        if (hasVisionOnMage || hasVisionOnWarrior)
            distanceFromTarget = iniDistanceFromTarget;
        else
            distanceFromTarget = 0;


        if (isAware && health > 0)
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
            else if (!AttackMode)
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

            if (((transform.position - warrior.transform.position).magnitude <= distanceFromTarget + 1.0f || (transform.position - mage.transform.position).magnitude <= distanceFromTarget + 1.0f) && navMeshAgent.velocity.x <= 0.5f && (hasVisionOnWarrior || hasVisionOnMage)) //Gestion Attaque
            {
                AttackMode = true;
                navMeshAgent.isStopped = true;
            } 
            else
            {
                navMeshAgent.isStopped = false;
                AttackMode = false;
            }
                

        }
    }

    public override void Attack()
    {
        //Stuff()    Update Animator to attack
        attackTimer = attackTimer - Time.deltaTime;

        if (attackTimer <= 0)
        {
            StartCoroutine(spriteObject.Flash());
            doOnce = true;
            attackTimer = attackSpeed;
            if (isLookingRight)
                Instantiate(EnnemyRangeAtk, new Vector3(transform.position.x + 0.7f, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
            else
                Instantiate(EnnemyRangeAtk, new Vector3(transform.position.x - 0.7f, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
        }
    }

    public override void UpdateAnimation()
    {
        doOnce = doOnce;
    }
}
