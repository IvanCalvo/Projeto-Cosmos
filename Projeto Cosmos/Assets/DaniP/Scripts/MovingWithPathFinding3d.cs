using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWithPathFinding3d : MonoBehaviour
{
    //Z IS THE FOWARD VECTOR

    [SerializeField]
    Transform target;

    [SerializeField]
    float speed = 10f;

    [SerializeField]
    float rotationSpeed = 1f;

    [SerializeField]
    float innerCircleRadius = 1f;

    [SerializeField]
    float outterCircleRaius = 3f;

    [SerializeField]
    float viewDistance = 5f;

    [SerializeField]
    int amountOfRays = 4;

    [SerializeField]
    List<Vector3> innerPointsPosition = new List<Vector3>();

    [SerializeField]
    List<Vector3> outterPointsPosition = new List<Vector3>();
    float angleAmount = 0;


    private void Start()
    {
        angleAmount = 2 * Mathf.PI / amountOfRays;        

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, innerCircleRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position+(transform.forward*viewDistance), outterCircleRaius);
    }

    private void Update()
    {
        Move();
        Pathfinding();
        
    }

    private void Turn()
    {
        if (target == null)
        {
            return;
        }   
        Vector3 direction = target.position - transform.position;
        Quaternion aimedRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Lerp(transform.rotation, aimedRotation, rotationSpeed * Time.deltaTime);

    }

    private void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
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
    
}
