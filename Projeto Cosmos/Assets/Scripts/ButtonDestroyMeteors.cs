using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDestroyMeteors : MonoBehaviour
{
    public AudioController audioController;
    Button missionButton;
    int state;
    public DestroyMeteors dm;
    public PlayerStats player;

    //0 - nao selecionada
    //1 - fazendo
    //2 - completa
    //3 - recebeu recompensa

    private void Awake()
    {
        if(PlayerPrefs.GetInt("hasPlayedBefore") == 1)
            state = PlayerPrefs.GetInt("DestroyMeteorState");
    }
    private void Start()
    {
        missionButton = GetComponent<Button>();
        ColorBlock cb = missionButton.colors;
        if (PlayerPrefs.GetInt("hasPlayedBefore") == 0)
        {
            state = 0;
            dm.state = false;
            cb.normalColor = Color.white;
        }
        if (state == 2)
        {
            cb.normalColor = Color.green;
            cb.selectedColor = cb.normalColor;
            missionButton.colors = cb;
        }
    }

    private void Update()
    {
        ColorBlock cb = missionButton.colors;
        if(dm.IsAchieved())
        {
            dm.complete = true;
            PlayerPrefs.SetInt("DestroyMeteorState", 2);
            state = 2;
        }
        
        if (state == 1)
        {
            cb.normalColor = Color.blue;
            cb.selectedColor = cb.normalColor;
            missionButton.colors = cb;
        }
        else if (state == 2)
        {
            cb.normalColor = Color.green;
            cb.selectedColor = cb.normalColor;
            missionButton.colors = cb;
        }
        else if (state == 3)
        {
            cb.normalColor = Color.yellow;
            cb.selectedColor = cb.normalColor;
            missionButton.colors = cb;
        }
        
    }

    public void clickButton()
    {
        ColorBlock cb1 = missionButton.colors;
        switch(state)
        {
            case 0:
                cb1.normalColor = Color.blue;
                audioController.ClickSound();
                state = 1;
                dm.state = true;
                PlayerPrefs.SetInt("DestroyMeteorState", 1);
                break;
            case 1:
                cb1.normalColor = Color.white;
                audioController.BackSound();
                state = 0;
                dm.state = false;
                PlayerPrefs.SetInt("DestroyMeteorState", 0);
                break;
            case 2:
                player.money += 150;
                audioController.CompleteMissionSound();
                state = 3;
                PlayerPrefs.SetInt("DestroyMeteorState", 3);
                break;
            default:
                break;
        }
        cb1.selectedColor = cb1.normalColor;
        missionButton.colors = cb1;
    }
    
}