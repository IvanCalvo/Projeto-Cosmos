using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TesteMovimento : MonoBehaviour
{
    // private float yawTorque = 500f;
    [SerializeField]private float pitchTorque = 1000f;
    [SerializeField] private float rollTorque = 1000f;
    [SerializeField] private float thrust = 100f;
    private float strafeThrust = 50f;
    [SerializeField]private float thrustGlideReduction = 100f;
    private float rollReduction = 0.111f;
    float glide = 0f;
    private Vector3 currentVelocity;
    [SerializeField] public float velocidade = 5f;



    Rigidbody rb;
    [SerializeField]private float inputRoll = 0f;
    [SerializeField]private float inputPitch = 0f;
    private float thrustInput = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        inputRoll = Input.GetAxis("Mouse X");
        inputPitch = Input.GetAxis("Mouse Y");
        thrustInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        // Colocar Drag = 1 e Angular Drag = 2.5 no RigidBody
         
        // Roll
        rb.AddRelativeTorque(Vector3.back * rollTorque * Mathf.Clamp(inputRoll, -1f, 1f) * Time.deltaTime);


        // Pitch
        rb.AddRelativeTorque(Vector3.right * pitchTorque * Mathf.Clamp(inputPitch, -1f, 1f) * Time.deltaTime);


        currentVelocity = new Vector3(Input.GetAxis("Horizontal") * velocidade * Time.deltaTime, 0f, Input.GetAxis("Vertical") * velocidade * Time.deltaTime);
        transform.Translate(currentVelocity);

        /*
        // Thrust
        if(thrustInput > 0.1f || thrustInput < -0.1f)
        {
            float currentThrust = thrust;

            rb.AddRelativeForce(currentThrust * thrustInput * Time.deltaTime * Vector3.forward);
            glide = thrust;
        }
        else
        {
            rb.AddRelativeForce(Vector3.forward * glide * Time.deltaTime);
            if(glide > 0.1f)
                glide -= thrustGlideReduction;
        }
        //*/
    }
}
