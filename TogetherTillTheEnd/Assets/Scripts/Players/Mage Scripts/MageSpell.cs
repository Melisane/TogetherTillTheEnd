using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpell : MonoBehaviour
{
    float time = 2.0f;
    public bool affectsWarrior = false;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate ()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            StartCoroutine(FadeOut());
        transform.Translate(Vector2.right * 10.0f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision col)    //For now deletes on any hit
    {
        if(col.gameObject.tag == "PlayerOne" && affectsWarrior)
        {
            col.gameObject.GetComponent<Warrior>().TakeDamage(1, false);
        }
        else       //If nothing uselful, delete
            Destroy(gameObject);
    }

    IEnumerator FadeOut()
    {
        animator.SetBool("Hit", true);
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CapsuleCollider2D>());
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}