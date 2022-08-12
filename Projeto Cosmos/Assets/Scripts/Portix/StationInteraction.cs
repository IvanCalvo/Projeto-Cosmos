using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationInteraction : MonoBehaviour
{
    public GameObject stationHUD;
    private bool isOnHUD = false;
    private void OnTriggerStay(Collider other) 
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F) && !isOnHUD)
        {
            stationHUD.SetActive(true);
            isOnHUD = true;
        } 
        else if(other.CompareTag("Player") && Input.GetKey(KeyCode.Escape) && isOnHUD)
        {
            stationHUD.SetActive(false);
            isOnHUD = false;
        }
    }

}
