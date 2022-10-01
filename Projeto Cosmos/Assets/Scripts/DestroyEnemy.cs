using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DestroyEnemy : Goal {
 
    public int enemyKilled = 0;
    public int enemyKilledGoal = 1;
    public bool displayOnce;
    public bool state;
    public bool complete;
    GUIStyle headStyle = new GUIStyle();

    private void Start()
    {
        if (PlayerPrefs.GetInt("hasPlayedBefore") == 1)
        {
            Debug.Log(PlayerPrefs.GetInt("DestroyEmemyState") + "EnemyState");
            if (PlayerPrefs.GetInt("DestroyEmemyState") == 1)
            {
                Debug.Log("1");
                state = true;
            }
        }
    }
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
            return (enemyKilled >= enemyKilledGoal);
        }
        else if(!complete)
        {
            enemyKilled = 0;
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
                GUILayout.Label(string.Format("Matou {0}/{1} inimigos (Completo!)", enemyKilled, enemyKilledGoal), headStyle);
            }
            else
            {
                GUILayout.Label(string.Format("Matou {0}/{1} inimigos", enemyKilled, enemyKilledGoal), headStyle);
            }
        }
    }

}