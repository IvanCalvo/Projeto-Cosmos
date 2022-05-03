using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public GameObject MenuCanvas;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
       
        MenuCanvas.SetActive(true);
    }
    public void StartGame()
    {
        SceneManager.LoadScene("DestroyEnemy Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}