using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSpawner : MonoBehaviour
{
    public Image starImg;
    public Canvas Canvas;
    float posX;
    float posY;
    public int numberOfStars;

    void Start()
    {
        numberOfStars = 2;
        generateSky();
    }

    void Update()
    {
        
    }

    void generateSky()
    {
        Image newStar;

        int i;
        for(i=0; i<numberOfStars; i++)
        {
            posX = Random.Range(-540f, 540f);
            posY = Random.Range(-250f, 250f);
            newStar = Instantiate(starImg);
            newStar.rectTransform.SetParent(Canvas.transform);
            newStar.rectTransform.position = new Vector3(posX, posY, 0f);
        }
    }
}
