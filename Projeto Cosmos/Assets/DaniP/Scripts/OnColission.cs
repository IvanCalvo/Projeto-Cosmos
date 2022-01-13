using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnColission : MonoBehaviour
{
    [SerializeField]
    private GameObject hitImpact = null;


    private void OnCollisionEnter(Collision collision)
    {
        if (hitImpact != null)
        {
            Instantiate(hitImpact, transform);
        }
    }


}
