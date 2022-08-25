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

    public TMP_Text text;


    // Update is called once per frame
    void Update() // Should Get informations from planet
    {
        try
        {
            GameObject planeta = gunScript.planeta;
            inViewScript = planeta.GetComponent<EnemyInView>();
            if (inViewScript.onScreen)
                text.enabled = true;
            else
                text.enabled = false;
            Vector3 planetPosition = new Vector3(planeta.transform.position.x + (planeta.transform.localScale.x*1.5f), planeta.transform.position.y , planeta.transform.position.z);
            gameObject.transform.position = cam.WorldToScreenPoint(planetPosition);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
        }
        catch(Exception e)
        {

        }
    }
}
