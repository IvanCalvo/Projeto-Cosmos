using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour
{
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

    public BoxCollider coll;
    public ShotEnemy ShEn;
    [SerializeField] float rotationSpeed = 1f;

    [SerializeField] float innerCircleRadius = 1f;

    [SerializeField] float outterCircleRaius = 3f;

    [SerializeField] float viewDistance = 5f;

    [SerializeField] int amountOfRays = 4;

    [SerializeField] List<Vector3> innerPointsPosition = new List<Vector3>();

    [SerializeField] List<Vector3> outterPointsPosition = new List<Vector3>();
    float angleAmount = 0;

    private void Awake()
    {
        angleAmount = 2 * Mathf.PI / amountOfRays;
        player = GameObject.Find("InicialShip").transform;
        enemyGun = GameObject.Find("enemyGun");
        enemyRB = GetComponent<Rigidbody>();
    }

    void DeffiningPointsInCircle(List<Vector3> circlePoints, float circleRadius, Vector3 circleCenter)
    {
        float angleAmount = 2*Mathf.PI / amountOfRays;

        for (int i = 0; i < amountOfRays; i++)
        {
            float x = Mathf.Cos(i * angleAmount)* circleRadius;
            float y = Mathf.Sin(i * angleAmount) * circleRadius;
            Vector3 pointPosition = new Vector3(x, y, 0);
            pointPosition += circleCenter;
            circlePoints.Add(pointPosition);

        }

    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange) Patroling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
    }

    void OnCollisionEnter(Collision collision) {
        ShEn.Shots++;
    }
    
    private void Turn()
    {
        if (player == null)
        {
            return;
        }   
        Vector3 direction = player.position - transform.position;
        Quaternion aimedRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, aimedRotation, rotationSpeed * Time.deltaTime);

    }

    private void Move()
    {
        transform.position += transform.forward * walkSpeed * Time.deltaTime;
    }

    private void Pathfinding()
    {
        DeffiningPointsInCircle(innerPointsPosition, innerCircleRadius, transform.position);
        DeffiningPointsInCircle(outterPointsPosition, outterCircleRaius, transform.position + (transform.forward * viewDistance));     

        Vector3 rayCastOffSet = Vector3.zero;
        Vector3 circleCenter = transform.position + (transform.forward * viewDistance);
        for (int i = 0; i < amountOfRays; i++)
        {
            RaycastHit hit;
            Vector3 dir = outterPointsPosition[i] - innerPointsPosition[i];
            if (Physics.Raycast(innerPointsPosition[i], dir,out hit ,viewDistance))
            {
                Vector3 amountToAdd = circleCenter - hit.point;
                amountToAdd.Normalize();
                Debug.Log(amountToAdd);
                rayCastOffSet += amountToAdd;
            }
        }
        if (rayCastOffSet!=Vector3.zero)
        {
            transform.Rotate(rayCastOffSet * Time.deltaTime);
        }
        else
        {
            Turn();
        }
    } 

    private void Patroling()
    {
        walkSpeed = 10f;
        Debug.Log("Patroling");
        if(!walkPointSet) SearchWalkPoint();
        Vector3 direction = walkPoint - this.transform.position;

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction, Vector3.up), turnSpeed * Time.deltaTime);

        /*Debug.Log("IDLE - Atual: " + this.transform.position);
        Debug.Log("walkpoint " + walkPoint);
        Debug.Log("direction " + direction);*/

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
        Debug.Log("Chasing");

        /*walkPointSet = false;
        Vector3 direction = player.position - this.transform.position;

        /*Debug.Log("CHASING - Direction: " + direction);
        Debug.Log("PlayerPosition: " + player.position);*/

        /*this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction, Vector3.up), turnSpeed * Time.deltaTime);
        
        transform.position += transform.forward * walkSpeed * Time.deltaTime;

        Vector3 desvio = new Vector3(direction.x + 1f, direction.y + 1f, direction.z + 1f);

        if(Physics.Raycast(player.position, -transform.up, 5f, Objects)) {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, 
                                                    Quaternion.LookRotation(desvio, Vector3.up), turnSpeed * Time.deltaTime);
            
            transform.position += transform.forward * walkSpeed * 2 * Time.deltaTime;
        }
        */
        walkSpeed = 10f;
        Pathfinding();
        Move();
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking");

        /*walkPointSet = false;

        bool objectAhead = false;

        Vector3 direction = player.position - this.transform.position;

        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, 
                                                    Quaternion.LookRotation(direction, Vector3.up), turnSpeed * Time.deltaTime);

        /*Debug.Log("ATTACKING - Direction: " + direction);
        Debug.Log("PlayerPosition: " + player.position);*/
        
        /*transform.position += transform.forward * walkSpeed/10 * Time.deltaTime;

        Vector3 desvio = new Vector3(direction.x + 1f, direction.y + 1f, direction.z + 1f);

        if(Physics.Raycast(player.position, -transform.up, 5f, Objects)) {
            objectAhead = true;
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation,
                                                    Quaternion.LookRotation(desvio, Vector3.up), turnSpeed * Time.deltaTime);
            
            transform.position += transform.forward * walkSpeed * Time.deltaTime;
        }

        transform.LookAt(player);*/
        walkSpeed = 1f;
        Pathfinding();
        Move();

        if(!alreadyAttacked)
        {
            Vector3 shootDirection = player.position - enemyGun.transform.position;
            //attack code
         
            //Debug.Log("ATTACK");
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


