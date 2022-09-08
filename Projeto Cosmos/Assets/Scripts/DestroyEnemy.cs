using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyEnemy : Goal {
 
    public int Shots = 0;
    public int ShotsGoal = 10;
    public bool displayOnce;
    public bool state;

    public GameObject enemyGO;

    public override void Complete() {
        if(displayOnce)
        {
            Debug.Log("Completo!");
        }
        displayOnce = false;
    }

    public override bool IsAchieved(){
        if(state) return (Shots >= ShotsGoal);

        return false;
    }
 
    public override void DrawHUD() {
        if(state)
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