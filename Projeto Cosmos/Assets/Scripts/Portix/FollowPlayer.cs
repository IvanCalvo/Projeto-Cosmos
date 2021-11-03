using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target = null;
    public Transform rig = null;

    public float distance = 2f;
    public float rotationSpeed = 10f;

    Vector3 cameraPosition;
    Vector3 smoothPosition;
    float smoothTime = 0.125f;
    float angle;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        BackCam();
    }

    void BackCam()
    {
        // Calcula a Posição
        cameraPosition = target.position - (target.forward * distance) + target.up * distance * 0.5f;
        smoothPosition = Vector3.Lerp(transform.position, cameraPosition, smoothTime);
        transform.position = smoothPosition;

        // Calcula a Rotação
        angle = Mathf.Abs(Quaternion.Angle(transform.rotation, target.rotation));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, (rotationSpeed + angle) * Time.deltaTime);
    }
}
