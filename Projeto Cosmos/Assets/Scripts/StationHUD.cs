using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationHUD : MonoBehaviour
{
    Button missionButton;
    int state;
    public DestroyMeteors dm;

    //0 - nao selecionada
    //1 - fazendo
    //2 - completa

    private void Start()
    {
        missionButton = GetComponent<Button>();
        state = 0;
        dm.state = false;
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
        if(dm.IsAchieved())
        {
            state = 2;
        }

        if (state == 2)
        {
            cb.normalColor = Color.green;
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
                dm.state = true;
                break;
            case 1:
                cb1.normalColor = Color.white;
                state = 0;
                dm.state = false;
                break;
            default:
                break;
        }
        cb1.selectedColor = cb1.normalColor;
        missionButton.colors = cb1;
    }
    
}