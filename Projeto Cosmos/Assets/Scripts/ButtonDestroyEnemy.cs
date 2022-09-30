using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDestroyEnemy : MonoBehaviour
{
    Button missionButton;
    int state;
    public DestroyEnemy de;
    public PlayerStats player;

    //0 - nao selecionada
    //1 - fazendo
    //2 - completa
    //3 - recebeu recompensa

    private void Awake()
    {
        state = PlayerPrefs.GetInt("DestroyEnemyState");
    }

    private void Start()
    {
        missionButton = GetComponent<Button>();
        state = 0;
        de.state = false;
        ColorBlock cb = missionButton.colors;
        cb.normalColor = Color.white;
        if(state == 2)
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
            PlayerPrefs.SetInt("DestroyEmemyState", 2);
            de.complete = true;
            state = 2;
        }

        if (state == 2)
        {
            cb.normalColor = Color.green;
            cb.selectedColor = cb.normalColor;
            missionButton.colors = cb;
        }
        if (state == 3)
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
                state = 1;
                de.state = true;
                PlayerPrefs.SetInt("DestroyEmemyState", 1);
                break;
            case 1:
                cb1.normalColor = Color.white;
                state = 0;
                de.state = false;
                PlayerPrefs.SetInt("DestroyEmemyState", 0);
                break;
            case 2:
                player.money += 50;
                state = 3;
                PlayerPrefs.SetInt("DestroyEmemyState", 3);
                break;
            default:
                break;
        }

        cb1.selectedColor = cb1.normalColor;
        missionButton.colors = cb1;
    }
    
}