using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeteors : Goal {
 
    public int meteorsDestroyed = 0;
    public int MeteorsGoal = 10;
    
    public override void Complete() {
        Debug.Log("Completo!");
    }

    public override bool IsAchieved(){
        return (meteorsDestroyed >= MeteorsGoal);
    }
 
    public override void DrawHUD() {
        if(this.IsAchieved())
            GUILayout.Label(string.Format("Destruir 10 meteoros: {0}/{1} (Completo!))", meteorsDestroyed, MeteorsGoal));
        else
            GUILayout.Label(string.Format("Destruir 10 meteoros: {0}/{1}", meteorsDestroyed, MeteorsGoal));
    }

}