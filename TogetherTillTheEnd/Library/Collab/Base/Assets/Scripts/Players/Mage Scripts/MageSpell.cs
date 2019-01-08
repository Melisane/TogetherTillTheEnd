using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpell : MonoBehaviour
{
    float time = 2.0f;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update ()
    {
        time -= Time.deltaTime;
        if (time <= 0)
            StartCoroutine(FadeOut());
        transform.Translate(Vector2.right * 10.0f * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D col)    //For now deletes on any hit
    {
        if (col.gameObject.tag == "Monster") //Check for monster or object
        {
            StartCoroutine(FadeOut());
            Destroy(col.gameObject);
        } 
        else       //If nothing uselful, delete
            Destroy(gameObject);
    }

    IEnumerator FadeOut()
    {
        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
