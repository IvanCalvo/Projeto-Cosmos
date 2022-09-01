using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpScript : MonoBehaviour
{
    public float maxHealth = 100;
    public float maxShield = 50;
    public float health = 100;
    public float shield = 50;

    public bool hasShield = false;

    private void Start()
    {
        if (hasShield)
        {
            health = maxHealth;
            shield = maxShield;
        }
        else
            health = maxHealth;
    }
    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
            health = 0;
        }
    }
}
