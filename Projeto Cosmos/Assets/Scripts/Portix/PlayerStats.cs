using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Referencias")]
    public HealthBar healthBarScript;
    public ShieldBar shieldBarScript;
    public TextMeshProUGUI moneyObject;
    public GameObject DeathCanvas;

    [Header("Stats")]
    public float maxHealth = 100;
    public float maxShield = 50;
    public float health = 100;
    public float shield = 50;

    private float recoverShieldTime = 5f;
    private float recoverHpDelay = 0.1f;
    private float recoverShieldDelay = 0.1f;


    public int isOnCombatTimer = 5;
    public int money = 0;

    public bool alive = true;
    public bool isOnCombat = false;

    // Start is called before the first frame update
    public void Start()
    {
        health = maxHealth;
        healthBarScript.SetMaxHealth(maxHealth);

        if (PlayerPrefs.GetInt("HasShield") == 1)
        {
            shield = maxShield;
            shieldBarScript.SetMaxShield(maxShield);
        } 
        else
        {
            shield = 0;
            shieldBarScript.SetShield(shield);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0 && alive)
        {
            alive = false;
            Die();
            health = 0;
        }
        healthBarScript.SetHealth(health);
        shieldBarScript.SetShield(shield);
        moneyObject.text = money.ToString(); // Maybe change only when receive money?

        if (Input.GetKey(KeyCode.M))
            money += 100;
    }

    public void buyShield()
    {
        PlayerPrefs.SetInt("HasShield", 1);
        shield = maxShield;
        shieldBarScript.SetMaxShield(maxShield);
        //shieldBarScript.SetShield(shield);
    }

    //*
    private void OnCollisionEnter(Collision collision) // Should Work
    {
        if(!collision.collider.CompareTag("Bullet") && !collision.collider.CompareTag("Missile") && !collision.collider.CompareTag("Untagged") && !collision.collider.CompareTag("Drop"))
        {
            CharacterController cControl = GetComponent<CharacterController>();
            float velocity = cControl.velocity.magnitude; // Gets current velocity to lose HP proportionally to its speed
            if (velocity > 20)
            {
                TakeDamage(velocity);
            }
        }
    }
    //*/

    public void TakeDamage(float damage)
    {
        isOnCombatTimer = 5;
        if (!isOnCombat)
            StartCoroutine(StopCombat());
        if (shield >= damage)
            shield -= damage;
        else if (shield <= damage && shield > 0)
        {
            float aux = damage - shield;
            shield = 0;
            health -= aux;
        }
        else if (health >= damage)
            health -= damage;
        else
            health = 0;
    }
    private void RecoverHp()
    {
        if (health <= maxHealth && alive && !isOnCombat)
        {
            health = Mathf.MoveTowards(health, maxHealth, 1000f * Time.deltaTime);
            Invoke("RecoverHp", recoverHpDelay);
            if (health == maxHealth)
                Invoke("RecoverShield", recoverShieldTime);
        }
    }

    private void RecoverShield()
    {
        if (shield <= maxShield && !isOnCombat && alive)
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

    private void Die()
    {
        DeathCanvas.SetActive(true);
        StationInteraction stationInterac = GameObject.FindGameObjectWithTag("StationInteraction").GetComponent<StationInteraction>();
        ShipMovement shipMov = gameObject.GetComponent<ShipMovement>();
        shipMov.boost_value = PlayerPrefs.GetInt("maxBoostValue");
        stationInterac.Pause();
    }


}
