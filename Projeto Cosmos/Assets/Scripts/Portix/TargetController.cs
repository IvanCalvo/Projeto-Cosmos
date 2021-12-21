using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    public Camera cam;
    EnemyInView target;
    public Image image;
    public GameObject player;

    public EnemyInView enemyScript;
    
    private bool lockedOn;

    private int lockedEnemy;

    public static List<EnemyInView> nearByEnemies = new List<EnemyInView>();

    void Start()
    {
        image = gameObject.GetComponent<Image>();

        lockedOn = false;
        lockedEnemy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null || !enemyScript.onScreen)
        {
            image.enabled = false; // Console ainda reclama que está tentando acessar o script... Resolve quando mudar o sistema de troca de lockOn
        }
        else if ( enemyScript.onScreen && lockedOn)
        {
            image.enabled = true;
        }

        float distancia = Vector3.Distance(player.transform.position, enemyScript.transform.position);
        Debug.Log(distancia);

        //Debug.Log("x: " + player.transform.position.x);
        //Debug.Log(target.enemyTransform.position.x);

        if (Input.GetKeyDown(KeyCode.Space) && !lockedOn && enemyScript.onScreen)
        {
            if(nearByEnemies.Count >= 1)
            {
                lockedOn = true;
                image.enabled = true;

                lockedEnemy = 0;
                target = nearByEnemies[lockedEnemy];
            }
        }

        else if(Input.GetKeyDown(KeyCode.Space) && lockedOn || nearByEnemies.Count == 0)
        {
            lockedOn = false;
            image.enabled = false;
            lockedEnemy = 0;
            target = null;
        }

        

        if(Input.GetKeyDown(KeyCode.X))
        {
            if (lockedEnemy == nearByEnemies.Count - 1)
            {
                lockedEnemy = 0;
                target = nearByEnemies[lockedEnemy];
            }
            else
            {
                lockedEnemy++;
                target = nearByEnemies[lockedEnemy];
            }
        }

        if(lockedOn)
        {
            target = nearByEnemies[lockedEnemy];

            gameObject.transform.position = cam.WorldToScreenPoint(target.transform.position);

        }

    }
}
