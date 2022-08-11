using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpScript : MonoBehaviour
{
    public float maxHealth = 100;
    public float maxShield = 50;
    public float health = 100;
    public float shield = 50;
    private float recoverHpTime = 5f;
    private float recoverShieldTime = 5f;
    private float recoverHpDelay = 0.2f;
    private float recoverShieldDelay = 0.3f;

    public int isOnCombatTimer = 5;

    public bool hasShield = false;
    public bool alive = true;
    public bool isOnCombat = false;

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
        // Checks if player hit certain objects
        if(gameObject.CompareTag("Player") && !other.CompareTag("Bullet") && !other.CompareTag("Missile") && !other.CompareTag("Untagged") && alive)
        {
            isOnCombatTimer = 5;
            if(!isOnCombat)
                StartCoroutine(StopCombat());
            CharacterController cControl = GetComponent<CharacterController>();
            float velocity = cControl.velocity.magnitude; // Gets current velocity to lose HP proportionally to its speed
            if (velocity > 20)
            {
                if(shield >= velocity)
                    shield -= velocity;
                else if(shield <= velocity && shield > 0)
                    shield = 0;
                else if (health >= velocity)
                    health -= velocity;
                else
                    health = 0;
            }
        }
    }
    
    private void RecoverHp()
    {
        if (health <= maxHealth - 1 && health > 0 && !isOnCombat)
        {
            health++;
            Invoke("RecoverHp", recoverHpDelay);
        }
        else if (health > maxHealth - 1 && health < maxHealth)
        {
            health = maxHealth;
            Invoke("RecoverShield", recoverShieldTime);
        }

    }

    private void RecoverShield()
    {
        if (shield <= maxShield - 1 && !isOnCombat)
        {
            shield++;
            Invoke("RecoverShield", recoverShieldDelay);
        }
        else if (shield > maxShield - 1 && shield < maxShield)
            shield = maxShield;
    }

    IEnumerator StopCombat()
    {
        isOnCombat = true;
        yield return new WaitForSeconds(1f);
        isOnCombatTimer--;
        if (isOnCombatTimer == 0)
        {
            isOnCombat = false;
            if (health < maxHealth)
                Invoke("RecoverHp", 0f);
            else if (shield < maxShield)
                Invoke("RecoverShield", 0f);
        }
        else if (isOnCombatTimer != 5 && isOnCombatTimer > 0)
            StartCoroutine(StopCombat());
        else if (isOnCombatTimer < 0)
            isOnCombatTimer = 0;
    }
}
