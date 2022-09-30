using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
    //ASSIGNABLES
    public Rigidbody rb;
    public GameObject explosion;
    public GameObject dropObject;
    public hpScript hpEnemyScript;
    public LayerMask whatIsEnemies;
    private TargetController targetControllerScript;

    //STATS
    [Range(0f, 1f)]
    public float bounciness;
    //public bool useGravity; // vai que corpos atraem a bala??

    //DANO
    public int bulletDamage;
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
        if ((collision.collider.CompareTag("Player") && explodeOnToutch))
        {
            Explode();

            PlayerStats playerStats = collision.collider.GetComponent<PlayerStats>();
            playerStats.TakeDamage(bulletDamage);
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
