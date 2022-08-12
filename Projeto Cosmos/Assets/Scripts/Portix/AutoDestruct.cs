using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestructTimer());
    }

    IEnumerator AutoDestructTimer()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
