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
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip backSound;

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
        audioSource.clip = clickSound;
        audioSource.Play();
        SceneManager.LoadScene("Portix");
    }

    public void Instructions()
    {
        MenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(true);
        audioSource.clip = clickSound;
        audioSource.Play();
        CreditsCanvas.SetActive(false);
    }

    public void Credits()
    {
        MenuCanvas.SetActive(false);
        InstructionCanvas.SetActive(false);
        audioSource.clip = clickSound;
        audioSource.Play();
        CreditsCanvas.SetActive(true);
    }

    public void BackToMenu()
    {
        MenuCanvas.SetActive(true);
        InstructionCanvas.SetActive(false);
        audioSource.clip = backSound;
        audioSource.Play();
        CreditsCanvas.SetActive(false);
    }

    public void QuitGame()
    {
        audioSource.clip = backSound;
        audioSource.Play();
        Application.Quit();
    }
}