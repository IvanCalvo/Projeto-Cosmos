using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDTips : MonoBehaviour
{
    public GameObject initialHUD;
    public GameObject[] initialTipsOrder;
    public GameObject[] stationTipsOrder;
    [SerializeField]int currentTipNumber = -1;
    [SerializeField]bool passedInitialTips = false;
    [SerializeField]bool passedStationTips = false;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("hasPlayedBefore") == 0)
        {
            PlayerPrefs.SetInt("isOnHUD", 1);
            Time.timeScale = 0f;
            initialTipsOrder[0].SetActive(true);
            InitialTips();
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    public void StartGame()
    {
        PlayerPrefs.SetInt("isOnHUD", 0);
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!passedInitialTips)
            InitialTips();
        if(!passedStationTips && PlayerPrefs.GetInt("FirstTimeOnStation") == 0 && PlayerPrefs.GetInt("isOnHUD") == 1)
            StationTips();
    }

    void InitialTips()
    {
        if (currentTipNumber >= initialTipsOrder.Length)
        {
            passedInitialTips = true;
            currentTipNumber = -1;
            StartGame();
        }
        else if (Input.anyKeyDown && currentTipNumber < initialTipsOrder.Length)
        {
            initialTipsOrder[currentTipNumber].SetActive(false);
            currentTipNumber++;
            try
            {
                initialTipsOrder[currentTipNumber].SetActive(true);
            }
            catch(Exception e)
            { }
            
        }   
    }

    public void StationTips()
    {
        if (currentTipNumber >= stationTipsOrder.Length)
        {
            passedStationTips = true;
            currentTipNumber = 0;
        }
        else if(currentTipNumber == -1)
        {
            stationTipsOrder[0].SetActive(true);
            currentTipNumber++;
        }
        else if (Input.anyKeyDown && currentTipNumber < stationTipsOrder.Length)
        {
            stationTipsOrder[currentTipNumber].SetActive(false);
            currentTipNumber++;
            try
            {
                stationTipsOrder[currentTipNumber].SetActive(true);
            }
            catch (Exception e)
            { }

        }
    }
}
