using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationInteraction : MonoBehaviour
{
    public CursorController cursorScript;

    public GameObject stationHUD;
    private bool isOnHUD = false;

    private void Update() 
    {
        if(Input.GetKey(KeyCode.Escape) && isOnHUD)
            Resume(); 
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F) && !isOnHUD) 
            Pause();
    }


    public void Resume()
    {
        stationHUD.SetActive(false);
        cursorScript.ChangeCursor(cursorScript.cursor);
        Time.timeScale = 1f;
        isOnHUD = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Pause()    // Talvez trocar o cursor? 
    {
        stationHUD.SetActive(true);
        cursorScript.ChangeCursor(cursorScript.cursorHUD);
        Time.timeScale = 0f;
        isOnHUD = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }


}
