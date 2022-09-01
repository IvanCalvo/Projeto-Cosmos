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

    private Vector3 planetPosition;

    private float defaultSizeText;
    int distanceFromPlayer;
    float objectScale = 0f;

    private void Start()
    {
        defaultSizeText = text.fontSize;
        objectScale = gameObject.transform.parent.localScale.x;
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

            text.fontSize = Mathf.Clamp(distanceFromPlayer/objectScale, 1f, 1000f) * 40f;
            distance.fontSize = text.fontSize - 8f;

            text.text = planetStatsScript.planetName;
            if (inViewScript.onScreen && gameObject.tag == planeta.tag)
            {
                distanceFromPlayer = (int)Vector3.Distance(player.transform.position, planeta.transform.position) -(int)planeta.transform.localScale.x/2;
                if (distanceFromPlayer >= 0)
                    distance.text = distanceFromPlayer.ToString() + "m";
                else
                    distance.text = "0m";
                distance.enabled = true;
                text.enabled = true;
            }
            else
            {
                distance.enabled = false;
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
