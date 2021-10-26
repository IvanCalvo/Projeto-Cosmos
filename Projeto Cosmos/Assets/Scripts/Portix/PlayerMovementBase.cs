using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBase : MonoBehaviour
{
    [SerializeField] public float velocidade = 5f;
    [SerializeField] public float rotacao = 50f;
    private Rigidbody playerRigidbody;
    private Vector3 currentVelocity;
    private Vector3 currentRotation;


    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        currentRotation = new Vector3(Input.GetAxis("Mouse Y") * 2 * rotacao * Time.deltaTime, 0f, -Input.GetAxis("Mouse X") * rotacao * Time.deltaTime);
        currentVelocity = new Vector3(Input.GetAxis("Horizontal") * velocidade * Time.deltaTime, 0f, Input.GetAxis("Vertical") * velocidade * Time.deltaTime);
        transform.Translate(currentVelocity);
        transform.Rotate(currentRotation);


    }
}
