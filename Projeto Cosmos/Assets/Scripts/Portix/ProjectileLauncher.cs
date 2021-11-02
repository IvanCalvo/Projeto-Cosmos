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
    private int magazine = 12;
    [SerializeField]public int bulletCount;
    [SerializeField]public int extraAmmo;
    private float reloadTime = 2f;

    [SerializeField]private bool isReloading = false;

    // Start is called before the first frame update
    void Start()
    {
        bulletCount = magazine;
        extraAmmo = 2 * magazine;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            extraAmmo += 5;
        if (isReloading)
            return;
        if (bulletCount <= 0 && extraAmmo > 0|| Input.GetKeyDown(KeyCode.R) && extraAmmo > 0 && bulletCount < magazine)
        { 
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= timeToFire && bulletCount !=0)
        {
            timeToFire = Time.time + 1 / fireRate;
            LaunchProjectile();
            bulletCount--;
        }

    }

    void LaunchProjectile()
    {
        var projectileInstance = Instantiate(projectileRb, firePoint.position, firePoint.rotation);

        projectileInstance.AddForce(firePoint.forward * launchForce);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);

        if ((magazine - bulletCount) > extraAmmo) // Arrumar, ainda está dando número negativo ??
        {
            bulletCount += extraAmmo;
            extraAmmo = 0;
        }
        else if (bulletCount != 0)
             {
                 extraAmmo -= (magazine - bulletCount);
                 bulletCount += (magazine - bulletCount);
             }
             else
             {
                 extraAmmo -= magazine;
                 bulletCount += magazine;
             }  

        isReloading = false;
    }
}
