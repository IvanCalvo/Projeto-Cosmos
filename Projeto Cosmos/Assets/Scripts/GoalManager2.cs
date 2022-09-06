using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager2 : MonoBehaviour
{
    public DestroyMeteors dm;
    public DestroyEnemy de;
    public bool dmAchieved;
    public bool deAchieved;
    public bool dmState;

    void Update()
    {
        dmAchieved = dm.IsAchieved();
        deAchieved = de.IsAchieved();
    }

    void changeState()
    {

    }
}
