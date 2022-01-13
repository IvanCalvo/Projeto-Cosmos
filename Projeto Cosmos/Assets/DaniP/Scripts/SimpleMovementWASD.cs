using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovementWASD : MonoBehaviour
{

    public float speed = 1f;


    private void Update()
    {
        float xDirection = Input.GetAxis("Horizontal");
        float zDirection = Input.GetAxis("Vertical");

        Vector3 moveDirecion = new Vector3(xDirection, 0f, zDirection);

        transform.position += moveDirecion * speed * Time.deltaTime;
    }







}