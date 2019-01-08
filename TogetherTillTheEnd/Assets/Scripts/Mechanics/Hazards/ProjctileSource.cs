using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjctileSource : MonoBehaviour {

    //enter the direction in the projectile
    [SerializeField]
    Vector3 projectileRelativeDirection;

    //EnterProjectileType
    [SerializeField]
    GameObject aProjectile;

    //Shoot Timer
    [SerializeField]
    float shootTimeInterval;
    float timeSinceLastShoot;

    //projectile Var
    [SerializeField]
    float projectileSpeed;

    private void Start()
    {
        timeSinceLastShoot = 0;   
    }

    // Update is called once per frame
    void Update () {

        if(timeSinceLastShoot + Time.deltaTime >= shootTimeInterval)
        {

            GameObject projectile = Instantiate(aProjectile, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity) as GameObject;
            HazardProjectile instancedProjectile = projectile.GetComponent<HazardProjectile>();
            instancedProjectile.SetDirection(projectileRelativeDirection);
            instancedProjectile.SetSpeed(projectileSpeed);
            timeSinceLastShoot = 0;
        }

        else
        {
            timeSinceLastShoot += Time.deltaTime;
        }
		
	}
}
