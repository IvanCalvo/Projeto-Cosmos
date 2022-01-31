using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeteors : Goal {
 
    public int MeteorsDestroyed = 0;
    public int MeteorsGoal = 50;
    
    public override void Complete() {
        Debug.Log("Completo!");
    }

    public override bool IsAchieved(){
        return (MeteorsDestroyed >= MeteorsGoal);
    }
 
    public override void DrawHUD() {
        GUILayout.Label(string.Format("Destruiu {0}/{1} meteoros", MeteorsDestroyed, MeteorsGoal));
    }

}