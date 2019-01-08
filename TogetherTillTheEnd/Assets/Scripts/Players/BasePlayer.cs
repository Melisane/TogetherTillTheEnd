using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class BasePlayer : MonoBehaviour
{
    //Gestion Movement
    protected float moveSpeed = 5.5f;
    protected bool isLookingRight = true;
    protected bool blockMovement = false;
    public const float EVADE_TIME = 1.5f;
    float evadeCD = EVADE_TIME;
    public bool isGrounded = false;


    //Gestion Abilities
    public GameObject RngAtk;
    public GameObject RngAtkPvP;
    public GameObject MeleeAtk;
    public GameObject MeleeAtkPvP;
    public bool hasSpecialAbility = false;
    public bool hasRangeAtk = false;
    public bool hasMeleeAtk = false;

    //Gestion Life
    [SerializeField]
    public int health = 3;
    public int maxHealth = 3;
    public bool isDead = false;
    protected float invincibilityFrames = 0.0f;
    public bool isMage = false;

    //Gestion Jump
    [SerializeField]
    protected float jumpForce = 14.0f;
    public int CanJump = 1;

    //Animation Transition
    protected float deltaTolerance = -0.05f;

    //External Refrences
    public Rigidbody rigidBody;
    protected Transform spriteChild;
    protected SpriteRenderer spriteRenderer;

    protected SharedCamera sharedCam;

    public virtual void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        sharedCam = FindObjectOfType<SharedCamera>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Update()
    {
        evadeCD -= Time.deltaTime;
        invincibilityFrames -= Time.deltaTime;
        if (!isDead || !blockMovement)
        {
            GestionInput();
        }
    }

    protected IEnumerator SetInvicibility(float time)
    {
        invincibilityFrames = time;
        spriteRenderer.color = new Color(1f, 1f, 1f, .5f);
        yield return new WaitForSeconds(time);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
    }

    public void UpdateGroundCheck(bool is_Grounded)
    {
        isGrounded = is_Grounded;
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "EnnemyAtkPhy")
        {
            TakeDamage(1, true);
        }
        if (col.gameObject.tag == "EnnemyAtkMag")
        {
            TakeDamage(1, false);
        }
    }

    protected void Evade()
    {
        if(evadeCD <= 0)
        {
            evadeCD = EVADE_TIME;
            StartCoroutine(SetInvicibility(.4f));
            if (isLookingRight)
                rigidBody.velocity = new Vector3(10, 0, 0);
            else
                rigidBody.velocity = new Vector3(-10, 0, 0);
        }
    }

    public abstract void TakeDamage(int nbOfDmg, bool IsPhysical);

    public abstract void GestionInput();
}
