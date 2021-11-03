using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target = null;
    public Transform rig = null;

    public float backDistance = 2f;
    public float upDistance = 1f;
    public float rotationSpeed = 5f;

    public Vector3 cameraOffset = new Vector3(0, -0.5f, 2f);

    [SerializeField]Vector3 cameraPosition;
    Vector3 smoothPositionUp;
    Vector3 smoothPositionBack;
    Vector3 smoothPosition;
    Vector3 smoothPositionClamp;
    public float smoothTime = 0.125f;
    public float angle;

    public ShipMovement shipMovement;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (shipMovement.boost_value > 20)
            smoothTime = 0.5f;
        else
            smoothTime = 0.125f;

        BackCam();

    }

    void BackCam()
    {
        // Calcula a Posição
        //*
        cameraPosition = target.position - (target.forward * backDistance) + target.up * upDistance ;
        smoothPosition = Vector3.Lerp(transform.position, cameraPosition, smoothTime);
        transform.position = smoothPosition;
        //*/


        // Calcula a Rotação
        angle = Mathf.Abs(Quaternion.Angle(transform.rotation, target.rotation));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, (rotationSpeed + angle) * Time.deltaTime);
    }

}
