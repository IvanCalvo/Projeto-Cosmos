using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDestroyEnemy : MonoBehaviour
{
    Button missionButton;
    public AudioController audioController;
    int state;
    public DestroyEnemy de;
    public PlayerStats player;

    //0 - nao selecionada
    //1 - fazendo
    //2 - completa
    //3 - recebeu recompensa

    private void Awake()
    {
        if (PlayerPrefs.GetInt("hasPlayedBefore") == 1)
        {
            state = PlayerPrefs.GetInt("DestroyEnemyState");
        }
    }


    private void Start()
    {
        missionButton = GetComponent<Button>();
        ColorBlock cb = missionButton.colors;
        if (PlayerPrefs.GetInt("hasPlayedBefore") == 0)
        {
            state = 0;
            de.state = false;
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
        if(de.IsAchieved())
        {
            PlayerPrefs.SetInt("DestroyEnemyState", 2);
            de.complete = true;
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
                de.state = true;
                PlayerPrefs.SetInt("DestroyEnemyState", 1);
                break;
            case 1:
                cb1.normalColor = Color.white;
                audioController.BackSound();
                state = 0;
                de.state = false;
                PlayerPrefs.SetInt("DestroyEnemyState", 0);
                break;
            case 2:
                player.money += 250;
                audioController.CompleteMissionSound();
                state = 3;
                PlayerPrefs.SetInt("DestroyEnemyState", 3);
                break;
            default:
                break;
        }

        cb1.selectedColor = cb1.normalColor;
        missionButton.colors = cb1;
    }
    
}