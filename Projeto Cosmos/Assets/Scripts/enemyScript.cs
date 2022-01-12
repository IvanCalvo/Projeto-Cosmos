using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour
{
    //public NavMeshAgent agent;

    Rigidbody enemyRB;

    public float walkSpeed;

    public Transform player;

    GameObject enemyGun;

    [SerializeField] GameObject CurrentProjectile;

    public LayerMask Objects, whatIsPlayer;

    public Vector3 walkPoint;
    [SerializeField] bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks = .3f;
    bool alreadyAttacked;
    public float turnSpeed = 100f;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("InicialShip").transform;
        enemyGun = GameObject.Find("enemyGun");
        enemyRB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if(!walkPointSet) SearchWalkPoint();
        Vector3 direction = walkPoint - this.transform.position;

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction, Vector3.up), turnSpeed * Time.deltaTime);

        Debug.Log("IDLE - Atual: " + this.transform.position);
        Debug.Log("walkpoint " + walkPoint);
        Debug.Log("direction " + direction);

        transform.position += transform.forward * walkSpeed * Time.deltaTime;

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        Vector3 desvio = new Vector3(direction.x + 1f, direction.y + 1f, direction.z + 1f);

        if(Physics.Raycast(player.position, -transform.up, 5f, Objects)) {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, 
                                                    Quaternion.LookRotation(desvio, Vector3.up), turnSpeed * Time.deltaTime);
            
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, Objects)) 
        {
            walkPointSet = false;
        
        } else {

            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        walkPointSet = false;
        Vector3 direction = player.position - this.transform.position;

        Debug.Log("CHASING - Direction: " + direction);
        Debug.Log("PlayerPosition: " + player.position);

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction, Vector3.up), turnSpeed * Time.deltaTime);
        
        transform.position += transform.forward * walkSpeed * Time.deltaTime;

        Vector3 desvio = new Vector3(direction.x + 1f, direction.y + 1f, direction.z + 1f);

        if(Physics.Raycast(player.position, -transform.up, 5f, Objects)) {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, 
                                                    Quaternion.LookRotation(desvio, Vector3.up), turnSpeed * Time.deltaTime);
            
            transform.position += transform.forward * walkSpeed * 2 * Time.deltaTime;
        }
    }

    private void AttackPlayer()
    {
        walkPointSet = false;

        bool objectAhead = false;

        Vector3 direction = player.position - this.transform.position;

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction, Vector3.up), turnSpeed * Time.deltaTime);

        Debug.Log("ATTACKING - Direction: " + direction);
        Debug.Log("PlayerPosition: " + player.position);
        
        transform.position += transform.forward * walkSpeed/10 * Time.deltaTime;

        Vector3 desvio = new Vector3(direction.x + 1f, direction.y + 1f, direction.z + 1f);

        if(Physics.Raycast(player.position, -transform.up, 5f, Objects)) {
            objectAhead = true;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,
                                                    Quaternion.LookRotation(desvio, Vector3.up), turnSpeed * Time.deltaTime);
            
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            Vector3 shootDirection = player.position - enemyGun.transform.position;
            //attack code
         
            Debug.Log("ATTACK");
            GameObject currentBullet = Instantiate(CurrentProjectile, enemyGun.transform.position, Quaternion.identity);
            currentBullet.GetComponent<Rigidbody>().AddForce(shootDirection.normalized * 200f, ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
