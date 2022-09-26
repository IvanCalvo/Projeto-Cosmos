using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationInteraction : MonoBehaviour
{
    public CursorController cursorScript;
    public ArmaRay gunScript;
    public PlayerStats playerStatsScript;

    public GameObject stationHUD;
    public bool isOnHUD = false;

    public Transform playerTransform;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("hasPlayedBefore") == 1)
        {
            LoadPlayerStatsAtStation();
        }
        else
            ResetStatsToDefault();
    }


    private void Update() 
    {
        if(Input.GetKey(KeyCode.Escape) && isOnHUD)
            Resume();
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F) && !isOnHUD)
        {
            SaveCurrentStats();
            Pause();
        }

        if (other.CompareTag("Player") && Input.GetKey(KeyCode.L) && !isOnHUD)
        {
            LoadPlayerStatsAtStation();
        }
    }


    public void Resume()
    {
        stationHUD.SetActive(false);
        cursorScript.ChangeCursor(cursorScript.cursor);
        Time.timeScale = 1f;
        isOnHUD = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        if(!gunScript.reloading && !gunScript.isOverHeating)  // To prevent player shooting while onPauseMenu/reloading/overheating
            gunScript.readyToShoot = true;
    }

    public void Pause()    // Talvez trocar o cursor? 
    {
        stationHUD.SetActive(true);
        cursorScript.ChangeCursor(cursorScript.cursorHUD);
        Time.timeScale = 0f;
        isOnHUD = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if(!gunScript.reloading && !gunScript.isOverHeating)
            gunScript.readyToShoot = false;
    }

    void SaveCurrentStats()
    {
        PlayerPrefs.SetInt("playerMoney", playerStatsScript.money);
        PlayerPrefs.SetFloat("posicaoX", playerTransform.position.x);
        PlayerPrefs.SetFloat("posicaoY", playerTransform.position.y);
        PlayerPrefs.SetFloat("posicaoZ", playerTransform.position.z);
        PlayerPrefs.SetFloat("rotacaoW", playerTransform.rotation.w);
        PlayerPrefs.SetFloat("rotacaoX", playerTransform.rotation.x);
        PlayerPrefs.SetFloat("rotacaoY", playerTransform.rotation.y);
        PlayerPrefs.SetFloat("rotacaoZ", playerTransform.rotation.z);
        PlayerPrefs.SetInt("hasPlayedBefore", 1);
        //SaveCurrentMission();
    }

    public void LoadPlayerStatsAtStation()
    {
        playerStatsScript.money = PlayerPrefs.GetInt("playerMoney");
        float posicaoX = PlayerPrefs.GetFloat("posicaoX");
        float posicaoY = PlayerPrefs.GetFloat("posicaoY");
        float posicaoZ = PlayerPrefs.GetFloat("posicaoZ");
        playerTransform.position = new Vector3(posicaoX, posicaoY, posicaoZ);
        float rotacaoW = PlayerPrefs.GetFloat("rotacaoW");
        float rotacaoX = PlayerPrefs.GetFloat("rotacaoX");
        float rotacaoY = PlayerPrefs.GetFloat("rotacaoY");
        float rotacaoZ = PlayerPrefs.GetFloat("rotacaoZ");
        playerTransform.rotation = new Quaternion(rotacaoX, rotacaoY, rotacaoZ, rotacaoW);
        playerStatsScript.health = playerStatsScript.maxHealth;
        playerStatsScript.shield = playerStatsScript.maxShield;
        playerStatsScript.alive = true;
        LoadCurrentMission();
    }
    public void ResetStatsToDefault()
    {
        PlayerPrefs.SetInt("playerMoney", 0);
        PlayerPrefs.SetFloat("posicaoX", 0);
        PlayerPrefs.SetFloat("posicaoY", 0);
        PlayerPrefs.SetFloat("posicaoZ", 0);
        PlayerPrefs.SetFloat("rotacaoW", 0);
        PlayerPrefs.SetFloat("rotacaoX", 0);
        PlayerPrefs.SetFloat("rotacaoY", 0);
        PlayerPrefs.SetFloat("rotacaoZ", 0);
        PlayerPrefs.SetInt("hasPlayedBefore", 0);
    }
    
    public void LoadCurrentMission()
    {

    }

}
