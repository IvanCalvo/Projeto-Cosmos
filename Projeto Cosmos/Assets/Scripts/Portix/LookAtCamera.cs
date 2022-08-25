using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour
{
    public EnemyInView inViewScript;
    public ArmaRay gunScript;
    public GameObject planeta;

    public Image image;

    void Update()
    {
        if (inViewScript.onScreen && gunScript.planeta == planeta) // Funcionando para se estiver na tela, depois modificar para quando estiver mirado nele.
        {
            image.enabled = true;
            transform.LookAt(Camera.main.transform.position, -Vector3.up);
        }
        else
            image.enabled = false;
    }
}
