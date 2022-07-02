using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
    //ASSIGNABLES
    public Rigidbody rb;
    public GameObject explosion;
    public hpScript enemyHP;
    public ShotEnemy shotGoal;
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
            Instantiate(explosion, transform.position, Quaternion.identity);

        //CHECK FOR ENEMY
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            //GET COMPONENT DOS INIMIGOS AGUI

            //EXEMPLO
            //enemies[i].GetComponent<ShootingAi>().TakeDamage(explosionDamage);
        }

        //ADD DELAY SO POR DEBUG
        Invoke("Delay", 0.05f);
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
        if ((collision.collider.CompareTag("Enemy") && explodeOnToutch) || (collision.collider.CompareTag("Planet") && explodeOnToutch))
        {
            Explode();
            //Destroy(collision.collider.gameObject);

            enemyHP.health -= 10;
            shotGoal.Shots++;

            if (hitImpactVFX != null)
            {
                Instantiate(hitImpactVFX, transform);
            }

            if (armaRayScript.hasOverHeat)
            {
                if (armaRayScript.overHeat <= 10)
                    armaRayScript.overHeat = 0;
                else
                    armaRayScript.overHeat -= 10;

                armaRayScript.isOverHeating = false;

                
            }
            else
                armaRayScript.extraAmmo += 2;
               
            if (collision.collider.gameObject == armaRayScript.inimigo)
            {
                targetControllerScript.firstLock = true;
                targetControllerScript.image.enabled = false;
                targetControllerScript.lockedOn = false;
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



}