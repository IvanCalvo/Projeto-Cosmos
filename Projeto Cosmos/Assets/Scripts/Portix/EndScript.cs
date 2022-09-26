using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    public void Respawn()
    {
        StationInteraction stationInterac = GameObject.FindGameObjectWithTag("StationInteraction").GetComponent<StationInteraction>();
        stationInterac.LoadPlayerStatsAtStation();
        //stationInterac.RespawnStats();
        stationInterac.Resume();
        gameObject.SetActive(false);
    }
}
