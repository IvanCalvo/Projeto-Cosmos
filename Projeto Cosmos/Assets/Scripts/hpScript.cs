using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpScript : MonoBehaviour
{
    public float health = 100;
    public bool alive = true;

    private void Update()
    {
        if(health <= 0 && alive)
        {
            if (gameObject.tag != "Player")
                Destroy(gameObject);
            else
                Debug.Log("Morreu");
            alive = false;
            health = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.CompareTag("Player") && (other.CompareTag("Planet") || other.CompareTag("Asteroid") || other.CompareTag("Enemy")) && alive)
        {
            CharacterController cControl = GetComponent<CharacterController>();
            float velocity = cControl.velocity.magnitude;
            if (velocity > 20)
            {
                if (health >= 20)
                    health -= velocity;
                else
                    health = 0;
            }
        }
    }
    
}
