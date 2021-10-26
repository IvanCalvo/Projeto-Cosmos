using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float velocidade = 25f;
    public float velocidadeAtual;
    public float aceleracaoPraFrente = 3f;
    private float mouseX, mouseY;

    private Vector2 posicaoMouse, centroTela, distanciaMouse;
    public float velocidadeRotacao = 45f, aceleracaoRotacao = 3f, rotacaoInput = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        centroTela.x = Screen.width * .5f;
        centroTela.y = Screen.height * .5f;
        //Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {
        posicaoMouse.x = Input.mousePosition.x;
        posicaoMouse.y = Input.mousePosition.y;

        distanciaMouse.x = (posicaoMouse.x - centroTela.x) / centroTela.y;
        distanciaMouse.y = (posicaoMouse.y - centroTela.y) / centroTela.x;

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");


        transform.Rotate(mouseY * velocidadeRotacao * Time.deltaTime, 0f, mouseX * -velocidadeRotacao * Time.deltaTime, Space.Self);

        velocidadeAtual = Mathf.Lerp(velocidadeAtual, Input.GetAxisRaw("Vertical") * velocidade, aceleracaoPraFrente * Time.deltaTime);

        transform.position += transform.forward * velocidadeAtual * Time.deltaTime;

        //currentRotation = new Vector3(Input.GetAxis("Mouse Y") * 2 * rotacao * Time.deltaTime, 0f, -Input.GetAxis("Mouse X") * rotacao * Time.deltaTime);
        //currentVelocity = new Vector3(Input.GetAxis("Horizontal") * velocidade * Time.deltaTime, 0f, Input.GetAxis("Vertical") * velocidade * Time.deltaTime);
        //transform.Translate(currentVelocity);
        //transform.Rotate(currentRotation);




    }
}
