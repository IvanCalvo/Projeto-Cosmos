using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpScript : MonoBehaviour
{
    public int health = 100;
    public GameObject enemyObject;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(health <= 0)
        {
            
            Destroy(enemyObject);
        }
    }
}
