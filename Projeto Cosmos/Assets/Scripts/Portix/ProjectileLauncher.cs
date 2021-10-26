using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]private Transform firePoint;
    [SerializeField]private Rigidbody projectileRb;
    private float launchForce = 500f;
    private float timeToFire = 0f;
    private float fireRate = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            timeToFire = Time.time + 1 / fireRate;
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        var projectileInstance = Instantiate(projectileRb, firePoint.position, firePoint.rotation);

        projectileInstance.AddForce(firePoint.forward * launchForce);
    }

}
