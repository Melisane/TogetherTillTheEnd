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
    public bool finalFight = false;

    Mage mage;

    //Animation
    public Animator animator;

    //Animation stuff
    bool IsRunning = false;
    bool IsTeleporting = false;
    bool IsFalling = false;
    bool IsJumping = false;
    bool IsTakingDmg = false;
    bool IsWalking = false;
    bool Sword = false;
    bool Bow = false;
    bool IsBerserkAnim = false;
    float Oldyposition;
    bool isInHand = false;

    //Audio
    public AudioClip[] footSteps;
    public AudioClip hitAudio;
    public AudioClip hurtAudio;
    public AudioClip rangeAtkAudio;
    public AudioClip berserkAudio;
    public AudioClip deathAudio;
    public AudioClip blockAudio;
    public AudioClip[] meleeAudio;
    AudioSource audioSource;


    //Gestion Atk
    float TIME_BETWEEN_SHOTS_Melee = 0.5f, TIME_BETWEEN_SHOTS_Range = 1.0f;
    float shotTimer = 0.0f;

    //Abilities
    public bool HasDoubleJumpAbility;
    public bool IsBerserk = false;
    bool takemage = false;
    bool collide = false;

    public override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        spriteChild = transform.Find("Warrior");
        animator = GetComponent<Animator>();
        mage = GameObject.Find("Mage").GetComponent<Mage>();
    }

    public void FixedUpdate() //For movement input only, so players dont glitch with walls and such
    {
        if (Input.GetAxis("HorizontalJoystick-PlayerOne") == 0)
            IsRunning = false;

        if (Input.GetButtonUp("Left-PlayerOne") || Input.GetButtonUp("Right-PlayerOne"))
        {
            IsRunning = false;
        }

        if ((Input.GetAxis("HorizontalJoystick-PlayerOne") > 0.5f || Input.GetButton("Right-PlayerOne")) && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            isLookingRight = true;
            IsRunning = true;
            FaceDirection(Vector2.right);
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if ((Input.GetAxis("HorizontalJoystick-PlayerOne") < -0.5f || Input.GetButton("Left-PlayerOne")) && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            isLookingRight = false;
            IsRunning = true;
            FaceDirection(Vector2.left);
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    public override void Update()
    {

        shotTimer -= Time.deltaTime;
        base.Update();
        if (!isDead)
        {
            if (CanJump == 1 && HasDoubleJumpAbility)
                secondJumpTimer -= Time.deltaTime;
            else
                secondJumpTimer = 0.2f;
        }

        if (takemage && IsBerserk)
        {
            isInHand = true;
            mage.transform.position = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
            mage.transform.rotation = Quaternion.Euler(mage.transform.rotation.x, mage.transform.rotation.y + 1, 90);
        }

        UpdateAnimator();
    }


    public override void GestionInput()
    {
        if (Input.GetButtonDown("Jump-PlayerOne") && CanJump >= 1)
        {
            if (HasDoubleJumpAbility && CanJump == 1)
            {
                if (secondJumpTimer <= 0)
                {
                    //animator.SetTrigger("IsJumping");     //Double Jump
                    CanJump--;
                    rigidBody.velocity = new Vector2(0, jumpForce);
                }
            }
            else
            {
                animator.SetTrigger("IsJumping");
                CanJump--;
                rigidBody.velocity = new Vector2(0, jumpForce);
            }

        }

        if (Input.GetButtonDown("Special-PlayerOne") && hasSpecialAbility)
        {
            if (!IsBerserk)
            {
                animator.SetTrigger("Berserk");
                StartCoroutine(Berserk());
            }

        }


        if (Input.GetButtonDown("Evade-PlayerOne"))
        {
            Evade();
        }

        if (Input.GetButtonDown("Shield-PlayerOne") && !shieldActive && hasShield)
        {
            StartCoroutine(SpawnShield());
        }

        if (Input.GetButton("Range-PlayerOne") && hasRangeAtk && shotTimer <= 0)
        {
            if (!Bow)
                StartCoroutine(FireArrow());
        }

        if (Input.GetButtonDown("Melee-PlayerOne") && hasMeleeAtk && shotTimer <= 0)
        {
            shotTimer = TIME_BETWEEN_SHOTS_Melee;
            StartCoroutine(SpawnMeleeAtk());
        }

        if (Input.GetButtonDown("Special-PlayerOne") && IsBerserk && mage.isInRock && collide)
        {

            takemage = true;

        }

        if (Input.GetButtonUp("Special-PlayerOne"))
        {
            if (isInHand)
            {
                takemage = false;

                if (isLookingRight)
                    mage.rigidBody.AddForce(new Vector3(1, 0.2f,0) * jumpForce * 1.5f);
                else
                    mage.rigidBody.AddForce(new Vector3(-1, 0.2f,0) * jumpForce * 1.5f);

                mage.transform.rotation = Quaternion.Euler(mage.transform.rotation.x, mage.transform.rotation.y + 1, 0);
                isInHand = false;
            }
        }

    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "PlayerTwo")
        {
            collide = true;
        }

    }

    public override void TakeDamage(int nbOfDmg, bool IsPhysical)
    {
        if (!isDead)
        {
            if (invincibilityFrames <= 0)
            {
                if (!(shieldActive && IsPhysical))    //If the atk IS physical and the shield is active, then dmg block
                {
                    audioSource.PlayOneShot(hitAudio, 0.4f);
                    Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                    StartCoroutine(SetInvicibility(2.0f));
                    IsTakingDmg = true;
                    health -= nbOfDmg;
                    if (health <= 0)
                    {
                        health = 0;
                        isDead = true;
                        animator.SetTrigger("Dies");
                        audioSource.PlayOneShot(deathAudio, 0.4f);
                    }
                    else if (!isDead)
                        audioSource.PlayOneShot(hurtAudio, 0.3f);
                }

                else
                    audioSource.PlayOneShot(blockAudio, 0.4f);
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
        if (!IsRunning && !IsFalling)
            animator.SetTrigger("MeleeAtk");
        yield return new WaitForSeconds(0.15f);
         audioSource.PlayOneShot(meleeAudio[Random.Range(0, 2)], 0.2f);
        if (isLookingRight)
            {
                if (finalFight)
                    Instantiate(MeleeAtkPvP, new Vector3(transform.position.x + 1.2f, transform.position.y, transform.position.z), transform.rotation);
                else
                    Instantiate(MeleeAtk, new Vector3(transform.position.x + 1.2f, transform.position.y, transform.position.z), transform.rotation);
            }
            else
            {
                if (finalFight)
                    Instantiate(MeleeAtkPvP, new Vector3(transform.position.x - 1.2f, transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 180));
                else
                    Instantiate(MeleeAtk, new Vector3(transform.position.x - 1.2f, transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 180));
            }

    }

    IEnumerator FireArrow()
    {
        animator.SetTrigger("RangeAttack");
        shotTimer = TIME_BETWEEN_SHOTS_Range;
        yield return new WaitForSeconds(0.5f);
        audioSource.PlayOneShot(rangeAtkAudio, 0.5f);
        if (isLookingRight)
        {
            if (finalFight)
                Instantiate(RngAtkPvP, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.1f, transform.position.z), transform.rotation);
            else
                Instantiate(RngAtk, new Vector3(transform.position.x + 0.5f, transform.position.y + 0.1f, transform.position.z), transform.rotation);
        }
        else
        {
            if (finalFight)
                Instantiate(RngAtkPvP, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.1f, transform.position.z), transform.rotation);
            else
                Instantiate(RngAtk, new Vector3(transform.position.x - 0.5f, transform.position.y + 0.1f, transform.position.z), transform.rotation);
        }
    }

    IEnumerator Berserk()
    {
        IsBerserk = true;
        audioSource.PlayOneShot(berserkAudio, 0.5f);
        spriteRenderer.color = new Color(1.0f, 0.2f, 0.1f, 1f);
        yield return new WaitForSeconds(10.0f);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        IsBerserk = false;
    }


    void UpdateAnimator()
    {
        if (isGrounded)
        {
            if (HasDoubleJumpAbility)
                CanJump = 2;
            else
                CanJump = 1;
            animator.SetBool("IsFalling", false);
        }
        else
        {
            animator.SetBool("IsFalling", true);
            CanJump = 0;
        }

        animator.SetBool("IsRunning", IsRunning);
    }

    void PlayFootStep(int step)
    {
        audioSource.PlayOneShot(footSteps[step], 0.3f);
    }
}
