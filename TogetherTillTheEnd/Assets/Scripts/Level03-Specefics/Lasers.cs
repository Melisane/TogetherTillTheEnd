using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lasers : MonoBehaviour {

    [SerializeField]
    GameObject ProjectilePrefab;
    public float timeBetweenBullets;
    float timer;
    public float bulletSpawnPosition;

    // Use this for initialization
    void Start()
    {
        timer = timeBetweenBullets;

    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0) {

            projectileCreation();
            timer = timeBetweenBullets;
        }
    }

    void projectileCreation()
    {

        Instantiate(ProjectilePrefab, new Vector3(transform.position.x + bulletSpawnPosition, transform.position.y, transform.position.z), Quaternion.identity);

    }
       
 

}
