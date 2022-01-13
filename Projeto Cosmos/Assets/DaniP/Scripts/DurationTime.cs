using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationTime : MonoBehaviour
{

    public float lifeTime = 10f;
    private float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(this);
        }
    }


}
