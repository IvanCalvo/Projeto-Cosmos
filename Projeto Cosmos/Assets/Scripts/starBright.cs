using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starBright : MonoBehaviour
{
    public GameObject star;
    bool started;
    float red;
    float green;
    float blue;

    void Start()
    {
        if(Random.Range(0f, 1f) < .2f)
        {
            //estrela azulada
            if(Random.Range(0f, 1f) < .5f)
            {
                red = Random.Range(.3f, .5f);
                green = Random.Range(0f, .3f);
                blue = Random.Range(.5f, .7f);
            } 
            else
            //estrela avermelhada
            {
                red = Random.Range(.5f, .8f);
                green = Random.Range(0f, .1f);
                blue = Random.Range(.2f, .5f);
            }
            
        }
        else
        {
            red = 1f;
            green = 1f;
            blue = 1f;
        }
        SpriteRenderer aux = star.GetComponent<SpriteRenderer>();
        aux.color = new Color(red, green, blue, Random.Range(.1f, 1f));
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
        sprite.color = new Color(red, green, blue, bright);
        started = false;
    }
}
