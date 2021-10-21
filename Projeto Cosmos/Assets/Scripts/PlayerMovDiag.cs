using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovDiag : MonoBehaviour
{
    public CharacterController controller;
    public Transform playerTransform;
    [SerializeField] public float speed = 12f;

    float xRotation = 0f;
    float yRotation = 0f;

    float zRotation = 0f;

    private float rollInput;
    public float lookSpeed = 60f;
    public float rollSpeed = 130f;

    public Vector2 lookInput, screenCenter, mouseDistance;
    // Start is called before the first frame update
    void Start()
    {
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift)) {
            speed = 50f;
        } else {
            speed = 12f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        rollInput = Mathf.Lerp(rollInput, Input.GetAxisRaw("Diagonal"), Time.deltaTime);

        transform.Rotate(-mouseDistance.y * lookSpeed * Time.deltaTime, mouseDistance.x * lookSpeed *Time.deltaTime, 
                        rollInput * rollSpeed * Time.deltaTime, Space.Self);

        // Movimento
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move* speed * Time.deltaTime);

    }
}
