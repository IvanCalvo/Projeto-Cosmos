using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetController : MonoBehaviour
{
    public Camera cam;
    public Image image;
    public GameObject player;

    public GameObject alvo;

    public EnemyInView enemyScript;
    public ArmaRay weaponScript;
    
    public bool lockedOn;
    public bool firstLock = true;
    public bool targetTracked;

    public int lockedEnemy;


    public static List<EnemyInView> nearByEnemies = new List<EnemyInView>();

    void Start()
    {
        //lockedOn = true;
        image = gameObject.GetComponent<Image>();
        lockedOn = false;
        targetTracked = false;
        //lockedEnemy = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (alvo == null) return;
        //*

        try {
            enemyScript = weaponScript.inimigo.GetComponent<EnemyInView>();
            if (!enemyScript.onScreen) // Caso o inimigo não esteja na tela, desaparece imagem de lock 
            {
                image.enabled = false; 
                if (lockedOn && !targetTracked) // Se estiver lockado, deixa de estar
                {
                    lockedOn = false;
                    image.color = Color.green;
                }
                weaponScript.inimigo = null; // Retira referência do inimigo
            }
            
            else if (enemyScript.onScreen && PlayerPrefs.GetInt("HasMissileGun") == 1)
            {
                image.enabled = true;
            }
            //*/

            if(Input.GetKeyDown(KeyCode.Mouse1) && PlayerPrefs.GetInt("HasMissileGun") == 1) // Sistema de Lock on
            {
                lockedOn = !lockedOn;
                if (lockedOn) // Muda a cor da mira
                    image.color = Color.red;
                else
                    image.color = Color.green;
            }


            if (weaponScript.readyToLock && PlayerPrefs.GetInt("HasMissileGun") == 1)
            {
                if (firstLock && enemyScript.onScreen)
                {
                    image.enabled = true;
                    firstLock = false;
                }
                alvo = weaponScript.inimigo;

                gameObject.transform.position = cam.WorldToScreenPoint(alvo.transform.position);

                //float distance = Vector3.Distance(player.transform.position, enemyScript.transform.position);
                //Debug.Log(distancia); // distancia até o inimigo

            }
        } catch (Exception e) { // Ao não achar mais o imigo que morreu, reseta cores padrões de mira, status de locked e seguido pelo missil
            image.color = Color.green;
            lockedOn = false;
            targetTracked = false;
            image.enabled = false;
        }
        

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
