using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationInteraction : MonoBehaviour
{
    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Player"))
            Debug.Log("Player Entrou");
    }

}
