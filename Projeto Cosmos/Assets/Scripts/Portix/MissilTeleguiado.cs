using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilTeleguiado : MonoBehaviour
{
    private float missileSpeed = 20f;
    private float accelerateActiveTime = 0f; // Quanto tempo está acelerando
    
    [SerializeField] private float missileAcceleration = 20f;
    [SerializeField] private float accelerateTime = 7f;
    [SerializeField] private float turnRate = 30000f; // estranho
    [SerializeField] private float trackingDelay = 1f;

    private bool missileActive = false;
    private bool isAccelarating = false;
    private bool targetTracking = false;

    //private Transform target; // Posição do alvo
    private Rigidbody rb;
    private Quaternion guideRotation;

    private TargetController targetControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        //target = GameObject.FindGameObjectWithTag("Planet").transform;
        rb = GetComponent<Rigidbody>();
        ActivateMissile();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject image = GameObject.FindGameObjectWithTag("LockOnImage");
        targetControllerScript = image.GetComponent<TargetController>();
        Run();
        GuidedMissile();
        //Debug.Log(targetControllerScript.target.transform.position);
    }

    private void ActivateMissile()
    {
        missileActive = true;
        accelerateActiveTime = Time.time;
        StartCoroutine(TargetTrackingDelay());
    }

    private void GuidedMissile()
    {
        //if (target == null) return;

        if(targetTracking)
        {
            Vector3 relativePosition = targetControllerScript.alvo.transform.position - transform.position;
            guideRotation = Quaternion.LookRotation(relativePosition, transform.up);
        }

        //Debug.Log("Tracking");
    }

    private void Run()
    {
        if (Since(accelerateActiveTime) > accelerateTime)
            isAccelarating = false;
        else
            isAccelarating = true;

        if (!missileActive) return;

        if (isAccelarating)
            missileSpeed += missileAcceleration * Time.deltaTime;

        rb.velocity = transform.forward * missileSpeed;

        if (targetTracking)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, guideRotation, turnRate * Time.deltaTime);
    }

    private float Since(float since)
    {
        return Time.time - since;
    }

    IEnumerator TargetTrackingDelay()
    {
        yield return new WaitForSeconds(Random.Range(trackingDelay, trackingDelay));
        targetTracking = true;
    }
}
