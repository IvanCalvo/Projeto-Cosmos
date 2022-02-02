using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShotEnemy : Goal {
 
    public int Shots = 0;
    public int ShotsGoal = 10;
    
    public override void Complete() {
        Debug.Log("Completo!");
    }

    public override bool IsAchieved(){
        return (Shots >= ShotsGoal);
    }
 
    public override void DrawHUD() {
        GUILayout.Label(string.Format("Acertou {0}/{1} tiros", Shots, ShotsGoal));
    }

}