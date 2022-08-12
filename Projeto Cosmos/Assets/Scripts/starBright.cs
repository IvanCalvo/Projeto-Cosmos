using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starBright : MonoBehaviour
{
    public GameObject star;
    bool started;

    void Start()
    {
        started = false;
    }

    void Update()
    {
        if (started==false)
        {
            StartCoroutine(brightTimer());
        }
    }

    IEnumerator brightTimer()
    {
        started = true;
        float time = Random.Range(3f, 13f);
        float bright = Random.Range(.1f, .6f);
        yield return new WaitForSeconds(time);
        SpriteRenderer sprite = star.GetComponent<SpriteRenderer>();
        sprite.color = new Color(1f, 1f, 1f, bright);
        started = false;
    }
}
