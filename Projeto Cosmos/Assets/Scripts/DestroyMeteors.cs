using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMeteors : Goal {
 
    public int meteorsDestroyed = 0;
    public int MeteorsGoal = 10;
    public bool displayOnce = true;
    public bool complete;
    public bool state;
    GUIStyle headStyle = new GUIStyle();

    public override void Complete() {
        if(displayOnce)
            Debug.Log("Completo!");
        displayOnce = false;
    }

    public override bool IsAchieved(){
        if (state && !complete)
        {
            //PlayerPrefs.SetInt("DestroyMeteorState", 1);
            return (meteorsDestroyed >= MeteorsGoal);
        }
        else if(!complete)
        {
            //PlayerPrefs.SetInt("DestroyMeteorState", 0);
            meteorsDestroyed = 0;
        }

        return false;
    }
 
    public override void DrawHUD() {
        headStyle.fontSize = 30;
        headStyle.normal.textColor = Color.white;

        if (state)
        {
            if (this.IsAchieved())
                GUILayout.Label(string.Format("Destruir 10 meteoros: {0}/{1} (Completo!))", meteorsDestroyed, MeteorsGoal), headStyle);
            else
                GUILayout.Label(string.Format("Destruir 10 meteoros: {0}/{1}", meteorsDestroyed, MeteorsGoal), headStyle);
        }
    }

}