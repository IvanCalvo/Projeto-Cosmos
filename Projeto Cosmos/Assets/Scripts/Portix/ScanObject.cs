using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ScanObject : MonoBehaviour
{
    public Camera cam;
    public GameObject alvo;
    public ArmaRay gunScript;

    private EnemyInView inViewScript;
    private PlanetStats planetStatsScript;

    public TMP_Text text;
    public TMP_Text distance;



    // Update is called once per frame
    void Update() // Should Get informations from planet
    {
        try
        {
            GameObject planeta = gunScript.planeta;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            inViewScript = planeta.GetComponent<EnemyInView>();
            planetStatsScript = planeta.GetComponent<PlanetStats>();
            text.text = planetStatsScript.planetName;
            if (inViewScript.onScreen)
            {
                int distanceFromPlayer = (int)Vector3.Distance(player.transform.position, planeta.transform.position);
                distance.text = distanceFromPlayer.ToString() + "m";
                distance.enabled = true;
                text.enabled = true;
            }
            else
            {
                distance.enabled = false;
                text.enabled = false;
            }
            Vector3 planetPosition = new Vector3(planeta.transform.position.x + (planeta.transform.localScale.x*1.5f), planeta.transform.position.y , planeta.transform.position.z);
            gameObject.transform.position = cam.WorldToScreenPoint(planetPosition);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }
        catch(Exception e)
        {

        }
    }
}
