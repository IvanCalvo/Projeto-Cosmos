using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationHUD : MonoBehaviour
{
    Button missionButton;
    int state;
    public bool changing;

    //0 - nao selecionada
    //1 - fazendo
    //2 - completa

    private void Start()
    {
        missionButton = GetComponent<Button>();
        state = 0;
        ColorBlock cb = missionButton.colors;
        cb.normalColor = Color.white;
        if(state == 2)
        {
            cb.normalColor = Color.green;
            cb.selectedColor = cb.normalColor;
            missionButton.colors = cb;
        }
    }

    public void clickButton()
    {
        changing = false;
        ColorBlock cb1 = missionButton.colors;
        switch(state)
        {
            case 0:
                cb1.normalColor = Color.blue;
                state = 1;
                break;
            case 1:
                cb1.normalColor = Color.white;
                state = 0;
                break;
            default:
                break;
        }
        cb1.selectedColor = cb1.normalColor;
        missionButton.colors = cb1;
    }
    
}