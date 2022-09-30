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

    float minStationClamp = 15f;
    [SerializeField]float minPlanetClamp;

    int distanceFromPlayer;

    private void Start()
    {
        minPlanetClamp = gameObject.transform.parent.localScale.x/30f;
    }


    // Update is called once per frame
    void Update() // Should Get informations from planet
    {
        try
        {
            GameObject planeta = gunScript.planeta;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            inViewScript = planeta.GetComponent<EnemyInView>();
            planetStatsScript = planeta.GetComponent<PlanetStats>();
            if(planeta.tag == "Planet")
                text.fontSize = Mathf.Clamp(distanceFromPlayer/20f, minPlanetClamp, 2000f);
            else
                text.fontSize = Mathf.Clamp(distanceFromPlayer/20f, minStationClamp, 2000f);

            if (inViewScript.onScreen && gameObject.tag == planeta.tag)
            {
                distanceFromPlayer = (int)Vector3.Distance(player.transform.position, planeta.transform.position) - (int)planeta.transform.localScale.x / 2;
                if (distanceFromPlayer >= 0)
                    text.text = planetStatsScript.planetName + "\n" + distanceFromPlayer.ToString() + "m";
                else
                    text.text = planetStatsScript.planetName + "\n" + "0m";
                text.enabled = true;
            }
            else
            {
                text.enabled = false;
            }
            //if (planeta.tag == "Planet")
            //planetPosition = new Vector3(planeta.transform.position.x + (planeta.transform.localScale.x * 1.5f), planeta.transform.position.y, planeta.transform.position.z);
            //else
            //planetPosition = new Vector3(planeta.transform.position.x + (planeta.transform.localScale.x), planeta.transform.position.y, planeta.transform.position.z) ;

            //gameObject.transform.position = cam.WorldToScreenPoint(planetPosition);
            //gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }
        catch(Exception e)
        {

        }
    }
}
