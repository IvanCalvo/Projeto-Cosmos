using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    string boost_string;
    [SerializeField] TMPro.TextMeshProUGUI boostText;
    public int boost_value;
    public CharacterController controller;
    public Transform playerTransform;
    [SerializeField] public float speed = 12f;

    private float rollInput;
    public float lookSpeed = 60f;
    public float rollSpeed = 130f;

    public Vector2 lookInput, screenCenter, mouseDistance;

    void Start()
    {
        boost_value = 1000000;
        boost_string = (boost_value / 10).ToString();
        boostText.text = boost_string;
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        boost_string = (boost_value / 10).ToString();
        boostText.text = boost_string;
        //Debug.Log(speed);
        if (boost_value < 0)
        {
            boost_value = 0;
        }
        if (Input.GetKey(KeyCode.LeftShift) && boost_value > 20)
        {
            speed = 36f;
            boost_value -= 10;
        }
        else
        {
            if (boost_value < 1000)
            {
                boost_value++;
                boost_value++;
            }
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

        transform.Rotate(-mouseDistance.y * lookSpeed * Time.deltaTime, mouseDistance.x * lookSpeed * Time.deltaTime,
                        rollInput * rollSpeed * Time.deltaTime, Space.Self);

        // Movimento
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

    }
}
