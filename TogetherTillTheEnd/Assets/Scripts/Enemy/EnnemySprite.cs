using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySprite : MonoBehaviour
{

    public GameObject body;
    public float heightDelta = 0.0f;
    SpriteRenderer spriteRenderer;
    Color originalTint = new Color(1f, 1f, 1f, 1f);

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void setTint(Color tint)
    {
        originalTint = tint;
        spriteRenderer.color = originalTint;
    }

    public IEnumerator Flash()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalTint;
    }

    public void FaceDirection(Vector2 direction)
    {
        Quaternion rotation3D = direction == Vector2.right ? Quaternion.LookRotation(Vector3.forward) : Quaternion.LookRotation(Vector3.back);
        this.transform.rotation = rotation3D;
    }



    void Update ()
    {
        transform.position = new Vector3(body.transform.position.x, body.transform.position.y + heightDelta, body.transform.position.z);
	}

}
