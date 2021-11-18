using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject asteroide;
    [SerializeField] private int quantidadeAsteroides = 300;
    private Vector3 posicaoAsteroide;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < quantidadeAsteroides; i++)
        {
            posicaoAsteroide = new Vector3(playerTransform.position.x + Random.Range(-700f, 700f) + 100, playerTransform.position.y + Random.Range(-700f, 700f) + 100, playerTransform.position.z + Random.Range(-700f, 700f) + 100);
            var spawn = Instantiate(asteroide, posicaoAsteroide, playerTransform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
