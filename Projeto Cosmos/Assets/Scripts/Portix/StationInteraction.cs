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
    public GameObject stationInteractionHint;
    public bool isOnHUD = false;

    public Transform playerTransform;

    bool playerIsOnTrigger;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("hasPlayedBefore") == 1)
        {
            LoadPlayerStatsAtStation();
            if (PlayerPrefs.GetInt("isOnHUD") == 1 )
            {
                PlayerPrefs.SetInt("isOnHUD", 0);
            }
        }
        else
            ResetStatsToDefault();

    }


    private void Update() 
    {
        if ((Input.GetKey(KeyCode.Escape) || Input.GetKeyDown(KeyCode.F)) && isOnHUD && playerStatsScript.alive)
        {
            SaveCurrentStats();
            Resume();
        }
        else if(Input.GetKeyDown(KeyCode.F) && PlayerPrefs.GetInt("isOnHUD") != 1 && playerStatsScript.alive && playerIsOnTrigger)
        {
            PlayerPrefs.SetInt("DestroyFirstMission", 1);
            SaveCurrentStats();
            Pause();
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Player") && !isOnHUD)
            stationInteractionHint.SetActive(true);
        if (other.CompareTag("Player") && playerStatsScript.alive) 
        {
            playerIsOnTrigger = true;
        }

        if (other.CompareTag("Player") && Input.GetKey(KeyCode.L) && !isOnHUD)
        {
            ResetStatsToDefault();
            LoadPlayerStatsAtStation();
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            stationInteractionHint.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt("FirstTimeOnStation") == 1)
        {
            PlayerPrefs.SetInt("FirstTimeOnStation", 0);
            playerStatsScript.money += 100;
        }
    }
    public void Resume()
    {
        stationHUD.SetActive(false);
        cursorScript.ChangeCursor(cursorScript.cursor);
        stationInteractionHint.SetActive(true);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("isOnHUD", 0);
        isOnHUD = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        if(!gunScript.reloading && !gunScript.isOverHeating)  // To prevent player shooting while onPauseMenu/reloading/overheating
            gunScript.readyToShoot = true;
    }

    public void Pause()    // Talvez trocar o cursor? 
    {
        stationInteractionHint.SetActive(false);
        if(playerStatsScript.alive)
            stationHUD.SetActive(true);
        cursorScript.ChangeCursor(cursorScript.cursorHUD);
        Time.timeScale = 0f;
        isOnHUD = true;
        PlayerPrefs.SetInt("isOnHUD", 1);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (!gunScript.reloading && !gunScript.isOverHeating)
            gunScript.readyToShoot = false;
    }

    void SaveCurrentStats()
    {
        PlayerPrefs.SetInt("playerMoney", playerStatsScript.money);
        PlayerPrefs.SetInt("bulletsLeft", gunScript.bulletsLeft);
        PlayerPrefs.SetInt("extraAmmo", gunScript.extraAmmo);
        PlayerPrefs.SetInt("hasPlayedBefore", 1);
        PlayerPrefs.SetFloat("posicaoX", playerTransform.position.x);
        PlayerPrefs.SetFloat("posicaoY", playerTransform.position.y);
        PlayerPrefs.SetFloat("posicaoZ", playerTransform.position.z);
        PlayerPrefs.SetFloat("rotacaoW", playerTransform.rotation.w);
        PlayerPrefs.SetFloat("rotacaoX", playerTransform.rotation.x);
        PlayerPrefs.SetFloat("rotacaoY", playerTransform.rotation.y);
        PlayerPrefs.SetFloat("rotacaoZ", playerTransform.rotation.z);
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
        gunScript.bulletsLeft = PlayerPrefs.GetInt("bulletsLeft");
        gunScript.extraAmmo = PlayerPrefs.GetInt("extraAmmo");
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
        PlayerPrefs.SetInt("DestroyMeteorState", 0);
        PlayerPrefs.SetInt("DestroyEnemyState", 0);
        PlayerPrefs.SetInt("FirstTimeOnStation", 1);
        PlayerPrefs.SetInt("DestroyFirstMission", 0);
        PlayerPrefs.SetInt("maxBoostValue", 200);
        PlayerPrefs.SetInt("HasShield", 0);
        PlayerPrefs.SetInt("BoostRefuelVelocity", 1);
        PlayerPrefs.SetInt("DropMultiplier", 1);
        PlayerPrefs.SetInt("HasMissileGun", 0);
        ShipMovement shipMov = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipMovement>();
        shipMov.boost_value = PlayerPrefs.GetInt("maxBoostValue");
        playerStatsScript.shield = 0;
        playerStatsScript.shieldBarScript.SetMaxShield(0);
    }


}
