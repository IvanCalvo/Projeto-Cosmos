using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementeTest : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerTransform;
    [SerializeField] public float speed = 12f;

    float xRotation = 0f;
    float yRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
      

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float rotationY = Input.GetAxis("Mouse X");
        float rotationX = Input.GetAxis("Mouse Y");
        // Rotação
        xRotation -= rotationX;
        yRotation -= rotationY;

        playerTransform.localRotation = Quaternion.Euler(-xRotation, 0f, yRotation);

        // Movimento
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move* speed * Time.deltaTime);

       


    }
}
