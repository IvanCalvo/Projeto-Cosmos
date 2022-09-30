using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
    //ASSIGNABLES
    public Rigidbody rb;
    public GameObject explosion;
    public GameObject dropObject;
    public hpScript hpEnemyScript;
    public DestroyEnemy shotGoalScript;
    public DestroyMeteors meteorGoalScript;
    public LayerMask whatIsEnemies;
    private ArmaRay armaRayScript;
    private TargetController targetControllerScript;

    //STATS
    [Range(0f, 1f)]
    public float bounciness;
    //public bool useGravity; // vai que corpos atraem a bala??

    //DANO
    public int explosionDamage;
    public float explosionRange;

    //LIFETIME / RANGE
    public int maxCollisions;
    public float maxlifeTime;
    public bool explodeOnToutch = true;
    public bool tookDamage = false;

    //VFX
    [SerializeField]
    GameObject hitImpactVFX = null;

    int collisions;
    PhysicMaterial physics_mat;
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
   
    private void Start()
    {
        GameObject gun = GameObject.FindWithTag("Gun");
        armaRayScript = gun.GetComponent<ArmaRay>();
        GameObject targetController = GameObject.FindGameObjectWithTag("LockOnImage");
        targetControllerScript = targetController.GetComponent<TargetController>();
        GameObject goalManager = GameObject.FindGameObjectWithTag("GoalManager");
        shotGoalScript = goalManager.GetComponent<DestroyEnemy>();
        meteorGoalScript = goalManager.GetComponent<DestroyMeteors>();
        Setup();
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

    private void Update()
    {
        //WHEN TO EXPLODE?
        //collision
        if (collisions > maxCollisions)
            Explode();
        //lifetime
        maxlifeTime -= Time.deltaTime;
        if (maxlifeTime <= 0)
            Explode();
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

    private void Explode()
    {
        //INSTANTIATE EXPLOSION
        if (explosion != null)
            Instantiate(explosion, transform.position, Quaternion.identity);   // Tem autoDestrutor?

        //CHECK FOR ENEMY
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            //GET COMPONENT DOS INIMIGOS AGUI

            //EXEMPLO
            //enemies[i].GetComponent<ShootingAi>().TakeDamage(explosionDamage);
        }

        //ADD DELAY SO POR DEBUG
        Invoke("Delay", 0f);
    }
    private void Delay()
    {
        Destroy(gameObject);
        
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

    private void OnCollisionEnter(Collision collision)
    {
        //CONTAR COLISAO
        collisions++;

        //EXPLODE IF HIT ENEMY
        if ((collision.collider.CompareTag("Enemy") && explodeOnToutch) || (collision.collider.CompareTag("Asteroid") && explodeOnToutch))
        {
            Explode();
            hpEnemyScript = collision.collider.GetComponent<hpScript>();

            if (!tookDamage)
            {
                if (hitImpactVFX != null)
                {
                    Instantiate(hitImpactVFX, transform);
                }
                if (collision.collider.gameObject == armaRayScript.inimigo)
                {
                    targetControllerScript.firstLock = true;
                    targetControllerScript.image.enabled = false;
                    targetControllerScript.lockedOn = false;
                }
                hpEnemyScript.health -= explosionDamage;
                if (hpEnemyScript.health <= 0)
                {
                    DropItem(collision);
                    try
                    {
                        if (collision.collider.CompareTag("Enemy"))
                            shotGoalScript.enemyKilled++;
                        else if (collision.collider.CompareTag("Asteroid"))
                            meteorGoalScript.meteorsDestroyed++;
                    }
                    catch (Exception e) { }
                }
                tookDamage = true;
            }
        }
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

    private void Setup()
    {
        //CREATE A NEW PHYSIC MATERIAL
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //ASSIGN MATERIAL TO COLLIDER
        if (gameObject.tag == "Bullet")
            GetComponent<CapsuleCollider>().material = physics_mat;
        else
            GetComponent<BoxCollider>().material = physics_mat;
    }
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);

    }

    private void DropItem(Collision objectTransform)
    {
        Instantiate(dropObject, objectTransform.collider.transform.position, Quaternion.identity);
    }


}