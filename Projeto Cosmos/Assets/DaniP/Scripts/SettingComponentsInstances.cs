using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingComponentsInstances : MonoBehaviour
{
    [SerializeField]
    private GameObject canvasGO;

    [SerializeField]
    TextMeshProUGUI boostText;

    [SerializeField]
    GameObject CanvasOverHeat;

    [SerializeField]
    //GameObject CanvasMunicaoMissil;


    private void Awake()
    {
        canvasGO = GameObject.Find("Canvas");
        boostText = canvasGO.GetComponentInChildren<TextMeshProUGUI>();
        ShipMovement shipMovement = GetComponent<ShipMovement>();
        shipMovement.boostText = this.boostText;

        //setting the combat Instances:
        ArmaRay armaRay = GetComponentInChildren<ArmaRay>();
        CanvasOverHeat = GameObject.Find("OverHeat");
        //CanvasMunicaoMissil = GameObject.Find()


    }

   
}
