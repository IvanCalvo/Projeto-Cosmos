using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToStationMission : Goal
{
    GUIStyle headStyle = new GUIStyle();

    public override void Complete()
    {
    }

    public override bool IsAchieved()
    {
        if (PlayerPrefs.GetInt("FirstTimeOnStation") == 0)
            return true;
        else
            return false;
    }

    public override void DrawHUD()
    {
        headStyle.fontSize = 30;
        headStyle.normal.textColor = Color.white;

        if (PlayerPrefs.GetInt("DestroyFirstMission") == 0)
        {
            if (IsAchieved())
                GUILayout.Label(string.Format("Chegou!"), headStyle);
            else
                GUILayout.Label(string.Format("Vá Até a Estação Principal"), headStyle);
        }
    }

}
