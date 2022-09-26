using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    string boost_string;
    public TMPro.TextMeshProUGUI boostText;
    public int boost_value;
    public CharacterController controller;
    public Transform playerTransform;
    [SerializeField] public float speed = 12f;
    public GameObject virtual_camera;
    Cinemachine.CinemachineVirtualCamera fov;

    private float rollInput;
    public float lookSpeed = 60f;
    public float rollSpeed = 130f;

    public Vector2 lookInput, screenCenter, mouseDistance;

    void Start()
    {
        if(PlayerPrefs.GetInt("hasPlayedBefore") == 0)
            PlayerPrefs.SetInt("maxBoostValue", 200);
        boost_value = PlayerPrefs.GetInt("maxBoostValue");
        boost_string = (boost_value / 10).ToString();
        boostText.text = boost_string;
        screenCenter.x = Screen.width * .5f;
        screenCenter.y = Screen.height * .5f;
        Cursor.lockState = CursorLockMode.Confined;
        controller.detectCollisions = false;

        fov = virtual_camera.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        boost_string = boost_value.ToString();
        boostText.text = boost_string;
        //Debug.Log(speed);
        if (boost_value < 0)
        {
            boost_value = 0;
        }
        if (Input.GetKey(KeyCode.LeftShift) && boost_value > 3)
        {
            Accelerate(36f, 80.0f, 1.0f);

            boost_value -= 2;
        }
        else
        {
            Accelerate(12f, 60.0f, 3.0f);

            if (boost_value < PlayerPrefs.GetInt("maxBoostValue"))
            {
                boost_value++;
            }
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

    void Accelerate(float desired_speed, float desired_fov, float lerp_strength) 
    {
        speed = Mathf.Lerp(speed, desired_speed, lerp_strength * Time.deltaTime);
        fov.m_Lens.FieldOfView = Mathf.Lerp(fov.m_Lens.FieldOfView, desired_fov, lerp_strength * Time.deltaTime);
    }

}
