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

    public LayerMask Objects, whatIsPlayer;

    public Vector3 walkPoint;
    [SerializeField] bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks = .3f;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("InicialShip").transform;
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
        Vector3 direction = walkPoint - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction), 1 * Time.deltaTime);

        Debug.Log("IDLE - Atual: " + this.transform.position);
        Debug.Log("walkpoint" + walkPoint);
        if(!walkPointSet) SearchWalkPoint();

        transform.position += transform.forward * walkSpeed * Time.deltaTime;
        //enemyRB.AddForce(walkPoint, ForceMode.Force);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

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
        Vector3 direction = player.position - this.transform.position;

        Debug.Log("CHASING - Direction: " + direction);
        Debug.Log("PlayerPosition: " + player.position);

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction), 1 * Time.deltaTime);
        
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
        //enemyRB.AddForce(player.position, ForceMode.Force);

        Vector3 desvio = new Vector3(player.position.x + 1f, player.position.y + 1f, player.position.z + 1f);

        if(Physics.Raycast(player.position, -transform.up, 5f, Objects)) {
            enemyRB.AddForce(desvio, ForceMode.Force);
        }
    }

    private void AttackPlayer()
    {

        Vector3 direction = player.position - this.transform.position;

        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction), Time.deltaTime);

        Debug.Log("ATTACKING - Direction: " + direction);
        Debug.Log("PlayerPosition: " + player.position);
        
        transform.position += transform.forward * walkSpeed/5 * Time.deltaTime;
        //enemyRB.AddForce(player.position, ForceMode.Force);

        Vector3 desvio = new Vector3(player.position.x + 1f, player.position.y + 1f, player.position.z + 1f);

        if(Physics.Raycast(player.position, -transform.up, 5f, Objects)) {
            enemyRB.AddForce(desvio, ForceMode.Force);
        }

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            //attack code
            Debug.Log("ATTACK");

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
