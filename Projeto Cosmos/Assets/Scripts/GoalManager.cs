using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour {
 
    public Goal[] goals;
 
    void Awake() {
        goals = GetComponents<Goal>();
    }
 
    void OnGUI() {
        foreach (var goal in goals) {
            goal.DrawHUD();
        }
    }
 
    void Update() {
        foreach (var goal in goals) {
            if (goal.IsAchieved()) {
                goal.Complete();
                Destroy(goal);
            }
        }
    }
}

public abstract class Goal : MonoBehaviour {
    public abstract bool IsAchieved();
    public abstract void Complete();
    public abstract void DrawHUD();
}
