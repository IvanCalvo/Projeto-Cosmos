using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInView : MonoBehaviour
{
    public Camera cam;
    bool addOnlyOnce;
    bool lockedOn;

    public bool onScreen;
    public Vector3 enemyPosition;
    void Start()
    {
        addOnlyOnce = true;
        cam = Camera.main;
        lockedOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        enemyPosition = cam.WorldToViewportPoint(gameObject.transform.position);
        // Debug.Log(enemyPosition);
        onScreen = enemyPosition.z > 0 && enemyPosition.x > 0 && enemyPosition.x < 1 && enemyPosition.y > 0 && enemyPosition.y < 1; // teste para adcionar todos
                                                                                                               // os inimigos do mapa 
        if(addOnlyOnce)
        {
            addOnlyOnce = false;
            TargetController.nearByEnemies.Add(this);
        }

        if (!onScreen) // Se não estiver na tela não pode dar lock -- talvez fique pesado
            lockedOn = false;
    }
}
