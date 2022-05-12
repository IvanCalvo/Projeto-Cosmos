using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
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
        if (Input.GetKeyDown(KeyCode.Escape))
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
    }

    void Pause()
    {
        PauseMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        Time.timeScale = 0f;
        GamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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