using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuCanvas;
    public GameObject InstructionCanvas;
    public GameObject CreditsCanvas;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
       
        MenuCanvas.SetActive(true);
        InstructionCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
    }
    public void StartGame()
    {
        //SceneManager.LoadScene("DestroyEnemy Scene");
        SceneManager.LoadScene("Portix");
    }

    public void Instructions()
    {
        MenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
    }

    public void Credits()
    {
        MenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        MenuCanvas.SetActive(true);
        InstructionCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}