using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatyObjects : MonoBehaviour
{
    float initialPosition;
    float current = 0.0f;
    public float speed = 0.1f;
    public float rangeDivider = 2;

	void Start ()
    {
        initialPosition = transform.position.y;
	}
	
	void Update ()
    {
        transform.position = new Vector3(transform.position.x,initialPosition + Mathf.Cos(current) / rangeDivider, transform.position.z);
        current += speed;
    }
}
