using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool activated = false;
    public bool movesInX = true;
    public float delta = 3.0f;
    Vector3 endPosition;
    Vector3 startPosition;
    public float moveSpeed = 3.0f;

    void Start()
    {
        startPosition = transform.position;
        if (movesInX)
            endPosition = new Vector3(transform.position.x + delta, transform.position.y, transform.position.z);
        else
            endPosition = new Vector3(transform.position.x, transform.position.y + delta, transform.position.z);
    }
	
	void Update ()
    {
        if (delta >= 0 && movesInX)
        {
            if(activated && transform.position.x < endPosition.x)
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
            if(!activated && transform.position.x > startPosition.x)
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
        }
        if (delta < 0 && movesInX)
        {
            if (activated && transform.position.x > endPosition.x)
                transform.Translate(Vector3.left * Time.deltaTime * moveSpeed);
            if (!activated && transform.position.x < startPosition.x)
                transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        }

        if (delta >= 0 && !movesInX)
        {
            if (activated && transform.position.y < endPosition.y)
                transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
            if (!activated && transform.position.y > startPosition.y)
                transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        }
        if (delta < 0 && !movesInX)
        {
            if (activated && transform.position.y > endPosition.y)
                transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
            if (!activated && transform.position.y < startPosition.y)
                transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }

    }
}
