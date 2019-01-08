using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : BasePlayer // WARRIOR
{
    //Gestion Jump
    float secondJumpTimer;
    public bool hasShield = false;
    bool shieldActive = false;
    public GameObject shield;
    public GameObject hitEffect;

    Mage mage;

    //Animation
    Animator animator;

    //Animation stuff
    bool IsRunning = false;
    bool IsTeleporting = false;
    bool IsFalling = false;
    bool IsJumping = false;
    bool IsTakingDmg = false;
    bool IsWalking = false;
    bool Sword = false;
    bool Bow = false;
    float Oldyposition;
 

    //Gestion Atk
    float TIME_BETWEEN_SHOTS = 1f;
    float shotTimer = 0.0f;

    //Abilities
    public bool HasDoubleJumpAbility;
    public bool IsBerserk = false;
    bool takemage = false;
    bool collide = false;

    public override void Start()
    {
        base.Start();
        spriteChild = transform.Find("Warrior");
        animator = GetComponent<Animator>();
        mage = GameObject.Find("Mage").GetComponent<Mage>();
    }

    public override void Update()
    {
        shotTimer -= Time.deltaTime;
        base.Update();
        if(!isDead)
        {
            if (CanJump == 1 && HasDoubleJumpAbility)
                secondJumpTimer -= Time.deltaTime;
            else
                secondJumpTimer = 0.2f;
        }

        if (takemage && IsBerserk) {
             mage.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
             mage.transform.rotation = Quaternion.Euler(mage.transform.rotation.x, mage.transform.rotation.y + 1, 90);
         }
         

        UpdateAnimator();
        Oldyposition = transform.position.y;

    }


    public override void GestionInput()
    {
        if (Input.GetAxis("HorizontalJoystick1") > 0 && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            IsRunning = true;
            isLookingRight = true;
            transform.Translate(Vector2.right * Input.GetAxis("HorizontalJoystick1") * moveSpeed * Time.deltaTime);
            FaceDirection(Vector2.right);
        }

        if (Input.GetAxis("HorizontalJoystick1") < 0 && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            IsRunning = true;
            isLookingRight = false;
            transform.Translate(Vector2.left * Input.GetAxis("HorizontalJoystick1") * moveSpeed * Time.deltaTime);
            FaceDirection(Vector2.left);
        }
        else if (Input.GetAxis("HorizontalJoystick1") == 0)
            IsRunning = false;

        if (Input.GetButton("LeftPlay1") && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            isLookingRight = false;
            IsRunning = true;
            transform.Translate(-Vector2.left * moveSpeed * Time.deltaTime);
            FaceDirection(Vector2.left);
        }

        if (Input.GetButtonUp("LeftPlay1")) {

            IsRunning = false;
        }

        if (Input.GetButton("RightPlay1") && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            isLookingRight = true;
            IsRunning = true;
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            FaceDirection(Vector2.right);
        }
        
        if (Input.GetButtonUp("RightPlay1"))
        {
            IsRunning = false;
        }

        

        if (Input.GetButtonDown("JumpPlay1") && CanJump >= 1)
        {

            IsJumping = true;


            if(HasDoubleJumpAbility && CanJump == 1)
            {
                if(secondJumpTimer <= 0)
                {
                    CanJump--;
                    rigidBody2D.velocity = new Vector2(0, 13);
                }
            }
            else
            {
                CanJump--;                
                rigidBody2D.AddForce(Vector2.up * jumpForce);
            }
            
        }

        if (Input.GetButtonDown("berserk") && hasSpecialAbility)
        {
            Debug.Log("Is Berserk Called");
            StartCoroutine(Berserk());
        }

        if (Input.GetButtonDown("WarriorShield") && !shieldActive && hasShield)
        {
            StartCoroutine(SpawnShield());
        }

        if (Input.GetButton("WarriorRangeAtk") && hasRangeAtk && shotTimer <= 0)
        {
            StartCoroutine(FireArrow());
        }

        if (Input.GetButtonDown("WarriorMeleeAtk") && hasMeleeAtk && shotTimer <= 0)
        {
            StartCoroutine(SpawnMeleeAtk());
        }

        if (Input.GetButtonDown("takeMage") && IsBerserk && mage.isInRock && collide) {

            takemage = true;

        }

        if (Input.GetButtonUp("takeMage")) {

            takemage = false;
            mage.transform.rotation = Quaternion.Euler(mage.transform.rotation.x, mage.transform.rotation.y + 1, 0);

        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {
       
        if(col.gameObject.tag == "Monster")
        {
            IsTakingDmg = true;
            TakeDamage(1, false);
        }

        if (col.gameObject.tag == "PlayerTwo")
        {
            collide = true;
        }
        
    }


    public void GroundCheck(bool isGrounded)
    {
        if (isGrounded)
        {
            if (HasDoubleJumpAbility)
                CanJump = 2;
            else
                CanJump = 1;
            animator.SetBool("IsFalling", false);
            IsFalling = false;
        }
        else
        {
            animator.SetBool("IsFalling", true);
            IsFalling = true;
        }

    }

    public void TakeDamage(int nbOfDmg, bool IsPhysical)
    {
        if (invincibilityFrames <= 0)
        {
            if (!(shieldActive && IsPhysical))    //If the atk IS physical and the shield is active, then dmg block
            {
                StartCoroutine(SetInvicibility(2.0f));
                Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                health -= nbOfDmg;
                if (health <= 0)
                {
                    health = 0;
                    isDead = true;
                }
            }
        }
    }


    private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        this.transform.rotation = rotation3D;
    }


    IEnumerator SpawnShield()
    {
        shieldActive = true;
        GameObject tempShield = Instantiate(shield, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        yield return new WaitForSeconds(2.0f);
        shieldActive = false;
        Destroy(tempShield);
    }

    IEnumerator SpawnMeleeAtk()
    {
        Sword = true;
        yield return new WaitForSeconds(.3f);
        if (isLookingRight)
            Instantiate(MeleeAtk, new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z), transform.rotation);
        else
            Instantiate(MeleeAtk, new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 180));
    }

    IEnumerator FireArrow()
    {
        shotTimer = TIME_BETWEEN_SHOTS;
        Bow = true;
        yield return new WaitForSeconds(0.5f);
        if (isLookingRight)
            Instantiate(RngAtk, new Vector3(transform.position.x + 1.0f, transform.position.y + 0.2f, transform.position.z), transform.rotation);
        else
            Instantiate(RngAtk, new Vector3(transform.position.x - 1.0f, transform.position.y + 0.2f, transform.position.z), transform.rotation);
    }

    IEnumerator Berserk() {

        IsBerserk = true;
        yield return new WaitForSeconds(5.0f);
        IsBerserk = false;

    }


    void UpdateAnimator() {

        animator.SetBool("MeleeAtk", Sword);
        animator.SetBool("RangeAtk", Bow);
        animator.SetBool("IsRunning", IsRunning);
        animator.SetBool("IsFalling", IsFalling);
        animator.SetBool("IsJumping", IsJumping);
        animator.SetBool("TakesDamage", IsTakingDmg);
       // animator.SetBool("IsWalking", IsWalking);
        animator.SetBool("HasShield", shieldActive);
        //animator.SetBool("Berserk", IsBerserk);


        Sword = false;
        Bow = false;
        IsJumping = false;
        IsTakingDmg = false;

        if (transform.position.y - Oldyposition < deltaTolerance)
        {
            IsFalling = true;
        }
    }


    void Die() {

        Destroy(gameObject);

    }
}
