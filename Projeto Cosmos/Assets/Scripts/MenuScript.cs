using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuCanvas;
    public GameObject InstructionCanvas;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
       
        MenuCanvas.SetActive(true);
        InstructionCanvas.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("DestroyEnemy Scene");
    }

    public void Instructions()
    {
        MenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        MenuCanvas.SetActive(true);
        InstructionCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}