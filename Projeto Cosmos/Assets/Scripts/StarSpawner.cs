using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarSpawner : MonoBehaviour
{
    public GameObject MainStar;
    public GameObject StarHUB;
    float posX;
    float posY;
    private int numberOfStars;

    void Start()
    {
        numberOfStars = 100;
        generateSky();
    }

    void Update()
    {
        
    }

    void generateSky()
    {
        GameObject newStar;

        int i;
        for(i=0; i<numberOfStars; i++)
        {
            posX = Random.Range(-100f, 100f);
            posY = Random.Range(-50f, 50f);
            Vector3 pos = new Vector3(posX, posY, 90f);
            newStar = Instantiate(MainStar);
            newStar.transform.SetParent(StarHUB.transform);
            newStar.transform.position = pos;
            SpriteRenderer starSprite = newStar.GetComponent<SpriteRenderer>();
            starSprite.color = new Color(1f, 1f, 1f, .3f);
        }
    }
}
