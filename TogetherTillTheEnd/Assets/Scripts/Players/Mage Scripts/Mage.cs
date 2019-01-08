using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : BasePlayer //MAGE
{
    public bool hasShield = false;
    bool shieldActive = false;
    public GameObject shield;
    public GameObject hitEffect;
    public bool finalFight = false;

    //Animation stuff
    public Animator animator;
    bool IsRunning = false;
    //Gestion Atk
    float TIME_BETWEEN_SHOTS = 0.5f;
    float shotTimer = 0.0f;

    public bool hasTeleport = false;
    public bool isInRock = false;

    //Audio
    public AudioClip[] footSteps;
    public AudioClip hitAudio;
    public AudioClip hurtAudio;
    public AudioClip[] rangeAtkAudio;
    public AudioClip dashAudio;
    public AudioClip deathAudio;
    public AudioClip blockAudio;
    public AudioClip meleeAudio;
    AudioSource audioSource;


    public override void Start()
    {
        base.Start();
        base.isMage = true;
        animator = GetComponent<Animator>();
        spriteChild = transform.Find("Mage");
        audioSource = GetComponent<AudioSource>();
    }


    public void FixedUpdate() //For movement input only, so players dont glitch with walls and such
    {
        if (Input.GetAxis("HorizontalJoystick-PlayerTwo") == 0)
            IsRunning = false;

        if (Input.GetButtonUp("Left-PlayerTwo") || Input.GetButtonUp("Right-PlayerTwo"))
        {
            IsRunning = false;
        }

        if ((Input.GetAxis("HorizontalJoystick-PlayerTwo") > 0.5f || Input.GetButton("Right-PlayerTwo")) && !sharedCam.PlayerReachedRightBoundary(transform.position.x))
        {
            isLookingRight = true;
            IsRunning = true;
            FaceDirection(Vector2.right);
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if ((Input.GetAxis("HorizontalJoystick-PlayerTwo") < -0.5f || Input.GetButton("Left-PlayerTwo")) && !sharedCam.PlayerReachedLeftBoundary(transform.position.x))
        {
            isLookingRight = false;
            IsRunning = true;
            FaceDirection(Vector2.left);
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Special-PlayerTwo") && hasSpecialAbility)   //Must be out of GestionMovement since will block the update
            RockForm();

        UpdateAnimator();
        shotTimer -= Time.deltaTime;
    }

    public override void GestionInput()
    {
        if (Input.GetButtonDown("Jump-PlayerTwo") && CanJump == 1)
        {
            CanJump--;
            isGrounded = false;
            rigidBody.velocity = new Vector3(0, jumpForce,0);
            animator.SetTrigger("IsJumping");
        }

        if (Input.GetButtonDown("Teleport-PlayerTwo") && hasTeleport && CanJump == 1 && isGrounded)
        {
            StartCoroutine(Teleport());
        }

        if (Input.GetButtonDown("Evade-PlayerTwo"))
        {
            Evade();
        }

        if (Input.GetButtonDown("Shield-PlayerTwo") && !shieldActive && hasShield)
        {
            StartCoroutine(SpawnShield());
        }

        if (Input.GetButtonDown("Range-PlayerTwo") && hasRangeAtk && shotTimer <= 0)
        {
            shotTimer = TIME_BETWEEN_SHOTS;
            StartCoroutine(SpawnRangeAtk());
        }

        if (Input.GetButtonDown("Melee-PlayerTwo") && hasMeleeAtk && shotTimer <= 0)
        {
            audioSource.PlayOneShot(meleeAudio, 0.4f);
            shotTimer = TIME_BETWEEN_SHOTS;
            StartCoroutine(SpawnMeleeAtk());

        }

    }

    void UpdateAnimator()
    {
        if (isGrounded)
        {
            CanJump = 1;
            animator.SetBool("Falls", false);
        } 
        else
        {
            animator.SetBool("Falls", true);
            CanJump = 0;
        }
        animator.SetBool("IsRunning", IsRunning);
    }

    public override void TakeDamage(int nbOfDmg, bool IsPhysical)
    {
        if (invincibilityFrames <= 0 && !isInRock && !isDead)
        {
            if (!(shieldActive && !IsPhysical))    //If the atk is not physical and the shild is active, then dmg block
            {
                audioSource.PlayOneShot(hitAudio, 0.4f);
                Instantiate(hitEffect, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
                health -= nbOfDmg;
                if (health <= 0)
                {
                    health = 0;
                    isDead = true;
                    animator.SetTrigger("Dies");
                    audioSource.PlayOneShot(deathAudio, 0.4f);
                }
                else
                {
                    animator.SetTrigger("TakesDamage");
                    audioSource.PlayOneShot(hurtAudio, 0.3f);
                    StartCoroutine(SetInvicibility(2.0f));
                }
                    
            }
            else
                audioSource.PlayOneShot(blockAudio, 0.4f);
        }
    }

    IEnumerator SpawnShield()
    {
        shieldActive = true;
        GameObject tempShield = Instantiate(shield, new Vector3(transform.position.x, transform.position.y, transform.position.z), transform.rotation);
        yield return new WaitForSeconds(2.0f);
        shieldActive = false;
        Destroy(tempShield);
    }

    IEnumerator SpawnRangeAtk()
    {
        if(!IsRunning)
            animator.SetTrigger("IsRangeAtacking");
        yield return new WaitForSeconds(.2f);
        audioSource.PlayOneShot(rangeAtkAudio[Random.Range(0, 2)], 0.5f);
        if (isLookingRight)
        {
            if (finalFight)
                Instantiate(RngAtkPvP, new Vector3(transform.position.x + 0.5f, transform.position.y - 0.2f, transform.position.z), transform.rotation);
            else
                Instantiate(RngAtk, new Vector3(transform.position.x + 0.5f, transform.position.y - 0.2f, transform.position.z), transform.rotation);
        }
        else
        {
            if (finalFight)
                Instantiate(RngAtkPvP, new Vector3(transform.position.x - 0.5f, transform.position.y - 0.2f, transform.position.z), transform.rotation);
            else
                Instantiate(RngAtk, new Vector3(transform.position.x - 0.5f, transform.position.y - 0.2f, transform.position.z), transform.rotation);
        }
    }

    IEnumerator SpawnMeleeAtk()
    {
        animator.SetTrigger("IsMeleeAtacking");
        yield return new WaitForSeconds(.3f);

        if (isLookingRight)
        {
            if (finalFight)
                Instantiate(MeleeAtkPvP, new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z), transform.rotation);
            else
                Instantiate(MeleeAtk, new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z), transform.rotation);
        }
        else
        {
            if (finalFight)
                Instantiate(MeleeAtkPvP, new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 180));
            else
                Instantiate(MeleeAtk, new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z), transform.rotation * Quaternion.Euler(0, 0, 180));
        }
    }

    private void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        this.transform.rotation = rotation3D;
    }

    void RockForm()
    {
        Animator anim = GetComponent<Animator>();
        isInRock = !isInRock;
        blockMovement = isInRock;

        if (isInRock)
        {
            anim.enabled = false;
            spriteRenderer.color = new Color(.53f, .21f, 0.0f, 1f);
            gameObject.layer = LayerMask.NameToLayer("RockForm");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Players");
            anim.enabled = true;
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
            

    }

    IEnumerator Teleport()
    {
        audioSource.PlayOneShot(dashAudio, 0.5f);
        animator.SetTrigger("IsTeleporting");
        blockMovement = true;
        if (isLookingRight)
            rigidBody.velocity = (Vector3.right * jumpForce * 0.9f);
        else
            rigidBody.velocity = (Vector3.left * jumpForce * 0.9f);
        rigidBody.velocity += (Vector3.up * jumpForce * 0.9f);
        gameObject.layer = LayerMask.NameToLayer("MageTeleport");
        yield return new WaitForSeconds(.6f);
        if(!finalFight)
            gameObject.layer = LayerMask.NameToLayer("Players");
        else
            gameObject.layer = LayerMask.NameToLayer("FinalFight-Mage");
        blockMovement = false;

    }

    void PlayFootStep(int step)
    {
        audioSource.PlayOneShot(footSteps[step], 0.3f);
    }
}
