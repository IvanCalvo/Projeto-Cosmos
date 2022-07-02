using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShotEnemy : Goal {
 
    public int Shots = 0;
    public int ShotsGoal = 10;
    public bool Dead;
    
    public override void Complete() {
        Debug.Log("Completo!");
    }

    public override bool IsAchieved(){
        return (Shots >= ShotsGoal);
    }
 
    public override void DrawHUD() {
        if(this.IsAchieved()) {
            GUILayout.Label(string.Format("Acertou {0}/{1} tiros (Completo!)", Shots, ShotsGoal));
        } else {
            GUILayout.Label(string.Format("Acertou {0}/{1} tiros", Shots, ShotsGoal));
        }
    }

}