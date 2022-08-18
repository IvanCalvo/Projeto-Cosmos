using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{   
    public StationInteraction stationScript;
    public ArmaRay gunScript;


    public static bool GamePaused = false;
    public GameObject PauseMenu;
    public GameObject OptionsMenu;


    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !stationScript.isOnHUD)
        {
            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        if(!gunScript.reloading && !gunScript.isOverHeating)    // To prevent player shooting while onPauseMenu/reloading/overheating
            gunScript.readyToShoot = true;
    }

    void Pause()
    {
        PauseMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if(!gunScript.reloading && !gunScript.isOverHeating)
            gunScript.readyToShoot = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Options()
    {
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }

    public void Backtopause()
    {
        PauseMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

}