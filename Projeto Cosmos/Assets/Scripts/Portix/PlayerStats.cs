using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public HealthBar healthBarScript;
    public ShieldBar shieldBarScript;

    public float maxHealth = 100;
    public float maxShield = 50;
    public float health = 100;
    public float shield = 50;

    private float recoverShieldTime = 5f;
    private float recoverHpDelay = 0.1f;
    private float recoverShieldDelay = 0.1f;


    public int isOnCombatTimer = 5;

    public bool hasShield = false;
    public bool alive = true;
    public bool isOnCombat = false;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        shield = maxShield;
        healthBarScript.SetMaxHealth(maxHealth);
        shieldBarScript.SetMaxShield(maxShield);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && alive)
        {
            Debug.Log("Morreu");
            alive = false;
            health = 0;
        }
        healthBarScript.SetHealth(health);
        shieldBarScript.SetShield(shield);
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
            Debug.Log("Emnt1");
        if(!collision.collider.CompareTag("Bullet") && !collision.collider.CompareTag("Missile") && !collision.collider.CompareTag("Untagged") && !collision.collider.CompareTag("Drop"))
        {
            Debug.Log("Emnt");
            isOnCombatTimer = 5;
            if (!isOnCombat)
                StartCoroutine(StopCombat());
            CharacterController cControl = GetComponent<CharacterController>();
            float velocity = cControl.velocity.magnitude; // Gets current velocity to lose HP proportionally to its speed
            if (velocity > 20)
            {
                if (shield >= velocity)
                    shield -= velocity;
                else if (shield <= velocity && shield > 0)
                {
                    float aux = velocity - shield;
                    shield = 0;
                    health -= aux;
                }
                else if (health >= velocity)
                    health -= velocity;
                else
                    health = 0;
            }
        }
    }
    //*/

    //*
    private void OnTriggerEnter(Collider other) // TRIGGER?????? 
    {
            Debug.Log("Emnt1");
        // Checks if player hit certain objects
        if (!other.CompareTag("Bullet") && !other.CompareTag("Missile") && !other.CompareTag("Untagged") && !other.CompareTag("Drop") && alive)
        {
            isOnCombatTimer = 5;
            if (!isOnCombat)
                StartCoroutine(StopCombat());
            CharacterController cControl = GetComponent<CharacterController>();
            float velocity = cControl.velocity.magnitude; // Gets current velocity to lose HP proportionally to its speed
            if (velocity > 20)
            {
                if (shield >= velocity)
                    shield -= velocity;
                else if (shield <= velocity && shield > 0)
                {
                    float aux = velocity - shield;
                    shield = 0;
                    health -= aux;
                }
                else if (health >= velocity)
                    health -= velocity;
                else
                    health = 0;
            }
        }
    }
    //*/
    private void RecoverHp()
    {
        if (health <= maxHealth && health > 0 && !isOnCombat)
        {
            health = Mathf.MoveTowards(health, maxHealth, 1000f * Time.deltaTime);
            Invoke("RecoverHp", recoverHpDelay);
            if (health == maxHealth)
                Invoke("RecoverShield", recoverShieldTime);
        }
    }

    private void RecoverShield()
    {
        if (shield <= maxShield && !isOnCombat)
        {
            shield = Mathf.MoveTowards(shield, maxShield, 1000f * Time.deltaTime); // Testing
            //shield++;
            Invoke("RecoverShield", recoverShieldDelay);
        }
        //else if (shield > maxShield - 1 && shield < maxShield)
        //shield = maxShield;
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
                Invoke("RecoverShield", recoverShieldTime);
        }
        else if (isOnCombatTimer != 5 && isOnCombatTimer > 0)
            StartCoroutine(StopCombat());
        else if (isOnCombatTimer < 0)
            isOnCombatTimer = 0;
    }


}
