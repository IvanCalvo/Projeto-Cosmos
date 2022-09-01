using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    private ArmaRay armaRayScript;
    private MoneyScript moneyScript;
    private PlayerStats playerStatScript;

    [SerializeField] private GameObject pickUpVFX;
    private float destructionTimer = 10f;

    private void Start()
    {
        StartCoroutine(AutoDestruction());
        GameObject gun = GameObject.FindWithTag("Gun");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerStatScript = player.GetComponent<PlayerStats>();
        armaRayScript = gun.GetComponent<ArmaRay>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(pickUpVFX, transform.position, Quaternion.identity);
            if (armaRayScript.hasOverHeat)
            {
                if (armaRayScript.overHeat <= 10)
                    armaRayScript.overHeat = 0;
                else
                    armaRayScript.overHeat -= 10;
                armaRayScript.isOverHeating = false;
            }
            else
                armaRayScript.extraAmmo += 2;
            playerStatScript.money += 10;
            Destroy(gameObject);
        }
    }

    IEnumerator AutoDestruction()
    {
        yield return new WaitForSeconds(destructionTimer);
        Destroy(gameObject);
    }
}
