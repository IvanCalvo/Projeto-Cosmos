using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeteors : Goal {
 
    public int meteorsDestroyed = 0;
    public int MeteorsGoal = 10;
    public bool displayOnce = true;
    public bool state;
    
    public override void Complete() {
        if(displayOnce)
            Debug.Log("Completo!");
        displayOnce = false;
    }

    public override bool IsAchieved(){
        if (state)
        {
            return (meteorsDestroyed >= MeteorsGoal);
        }

        return false;
    }
 
    public override void DrawHUD() {

        if (state)
        {
            if (this.IsAchieved())
                GUILayout.Label(string.Format("Destruir 10 meteoros: {0}/{1} (Completo!))", meteorsDestroyed, MeteorsGoal));
            else
                GUILayout.Label(string.Format("Destruir 10 meteoros: {0}/{1}", meteorsDestroyed, MeteorsGoal));
        }
    }

}