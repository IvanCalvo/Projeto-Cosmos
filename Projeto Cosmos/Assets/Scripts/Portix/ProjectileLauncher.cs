using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField]private Transform firePoint;
    [SerializeField]private Rigidbody projectilePrefab;
    private float launchForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        var projectileInstance = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        projectileInstance.AddForce(firePoint.forward * launchForce);
    }

}
