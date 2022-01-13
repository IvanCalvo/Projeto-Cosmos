using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovementWASD : MonoBehaviour
{

    public float speed = 1f;
    public Transform CameraPosition;
    private Transform MyPosition;
    Vector3 directionFoward;
    Vector3 directionSideWays;

    private void Start()
    {
        MyPosition = transform;
    }


    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        directionFoward = MyPosition.position - CameraPosition.position;
        directionSideWays = new Vector3(directionFoward.z, directionFoward.y, -1*directionFoward.x);

        transform.position += directionFoward * speed *vertical* Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(new Vector3(directionSideWays.x, 0, directionSideWays.z));


        //Vector3 moveDirecion = new Vector3(xDirection, 0f, zDirection);
        //transform.position += moveDirecion * speed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (100) * directionFoward);

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, transform.position + (100) * directionSideWays);

    }





}