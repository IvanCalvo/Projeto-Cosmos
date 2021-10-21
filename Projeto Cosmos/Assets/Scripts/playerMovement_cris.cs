using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//W e S controla a aceleracao da nave
//A e D controla o movimento em X
//Q e E controla a rotacao no proprio eixo
//Mouse controla a direção da nave


public class playerMovement_cris : MonoBehaviour
{
    //declaracoes locais

    //VELOCIDADE
    [SerializeField] public float velocidade = 5f;
    private Vector3 currentVelocity;
    
    //ROTACAO
    [SerializeField] public float velocidadeRotacao = 5f;
    private Vector3 currentRotation;
    public float isRotating = 0f;

    //OUTROS
    private Rigidbody playerRigidbody;
    private Transform myTransform; //vi que isso aqui ajuda no processamento do jogo nsei pq


    // Start is called before the first frame update
    void Start()
    {
        myTransform = transform;

        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //W e S controle da aceleracao


        //MOVIMENTO
        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //A e D movimento em x, esquerda e direita
        currentVelocity = new Vector3(Input.GetAxis("Horizontal") * velocidade * Time.deltaTime, 0f,Input.GetAxis("Vertical") * velocidade * Time.deltaTime);
        myTransform.Translate(currentVelocity);
        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//

        //ROTACAO
        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
        //Q= z+ e E=z-
        bool apertandoQ = Input.GetKey(KeyCode.Q);
        bool apertandoE = Input.GetKey(KeyCode.E);

        if ((!apertandoQ)||(!apertandoE))
            isRotating = 0f;
        if (apertandoQ)
            isRotating = 1f;
        if (apertandoE)
            isRotating = -1f;

        currentRotation = new Vector3(0f,0f,velocidadeRotacao * Time.deltaTime * isRotating);
        myTransform.Rotate(currentRotation);
        //-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=--=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-//
    }
}