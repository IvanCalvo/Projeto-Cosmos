using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInView : MonoBehaviour
{
    public Camera cam;
    bool addOnlyOnce;

    public bool onScreen;
    public Vector3 enemyPosition;
    void Start()
    {
        addOnlyOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        enemyPosition = cam.WorldToViewportPoint(gameObject.transform.position);
        // Debug.Log(enemyPosition);
        onScreen = enemyPosition.z > 0 && enemyPosition.x > 0 && enemyPosition.x < 1 && enemyPosition.y > 0 && enemyPosition.y < 1;

        if(onScreen && addOnlyOnce)
        {
            addOnlyOnce = false;
            TargetController.nearByEnemies.Add(this);
        }
        
    }
}