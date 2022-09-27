using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyEnemy : Goal {
 
    public int Shots = 0;
    public int ShotsGoal = 10;
    public bool displayOnce;
    public bool state;
    public bool complete;
    public GameObject enemyGO;
    GUIStyle headStyle = new GUIStyle();

    public override void Complete() {
        if(displayOnce)
        {
            Debug.Log("Completo!");
        }
        displayOnce = false;
    }

    public override bool IsAchieved(){
        if (state && !complete)
        {
            return (Shots >= ShotsGoal);
        }
        else if(!complete)
        {
            Shots = 0;
        }

        return false;
    }
 
    public override void DrawHUD() {
        headStyle.fontSize = 30;
        headStyle.normal.textColor = Color.white;
        if (state)
        {
            if (this.IsAchieved())
            {
                GUILayout.Label(string.Format("Acertou {0}/{1} tiros (Completo!)", Shots, ShotsGoal));
            }
            else
            {
                GUILayout.Label(string.Format("Acertou {0}/{1} tiros", Shots, ShotsGoal));
            }
        }
    }

}