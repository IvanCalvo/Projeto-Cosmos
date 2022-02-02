using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    public Camera cam;
    public EnemyInView target;
    public Image image;
    public GameObject player;

    public GameObject alvo;

    public EnemyInView enemyScript;
    public ArmaRay weaponScript;
    
    public bool lockedOn;
    public bool firstLock = true;

    public int lockedEnemy;

    public static List<EnemyInView> nearByEnemies = new List<EnemyInView>();

    void Start()
    {
        //lockedOn = true;
        image = gameObject.GetComponent<Image>();

        lockedOn = false;
        //lockedEnemy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (alvo == null) return;
        //*

        try {
            enemyScript = weaponScript.inimigo.GetComponent<EnemyInView>();
            if (!enemyScript.onScreen) // target == null || 
            {
                image.enabled = false; // Console ainda reclama que est� tentando acessar o script... Resolve quando mudar o sistema de troca de lockOn
            }
            
            else if (enemyScript.onScreen)
            {
                image.enabled = true;
            }
            //*/
            float distancia = Vector3.Distance(player.transform.position, enemyScript.transform.position);

            if(Input.GetKeyDown(KeyCode.Mouse1))
            {
                lockedOn = !lockedOn;
            }


            if (weaponScript.readyToLock)
            {
                if (firstLock)
                {
                    image.enabled = true;
                    firstLock = false;
                }
                alvo = weaponScript.inimigo;

                gameObject.transform.position = cam.WorldToScreenPoint(alvo.transform.position);

            }
        } catch (Exception e) {
            //Debug.Log("Objeto ainda nao encontrado ou destruido");
        }
        
        //Debug.Log(distancia);

        //Debug.Log("x: " + player.transform.position.x);
        //Debug.Log(target.enemyTransform.position.x);
        
        /*
        if (Input.GetKeyDown(KeyCode.Space) && !lockedOn && enemyScript.onScreen) // LockOn esta atrelado ao bot�o espa�o, mudar para botao direito do mouse
        {
            if(nearByEnemies.Count >= 1)
            {
                lockedOn = true;
                image.enabled = true;

                //lockedEnemy = 0;
                //target = nearByEnemies[lockedEnemy];
            }
        }

        else if(Input.GetKeyDown(KeyCode.Space) && lockedOn || nearByEnemies.Count == 0)
        {
            lockedOn = false;
            image.enabled = false;
            //lockedEnemy = 0;
            //target = null;
        }

        //*/

        
        // Desatualizado
        /*
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
        
        
        
        
        if (lockedOn)
        {
            target = nearByEnemies[lockedEnemy];

            gameObject.transform.position = cam.WorldToScreenPoint(target.transform.position);

        }



        //*/

        //Debug.Log(target.transform.position);

    }
}
