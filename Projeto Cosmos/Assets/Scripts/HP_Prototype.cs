using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP_Prototype : MonoBehaviour
{

    public BoxCollider mainCollider;
    
    void Start()
    {
        Time.timeScale = 1f;
        mainCollider = GetComponent<BoxCollider>();   
    }

    void OnTriggerEnter(Collider colliderObject) {
        if(colliderObject.gameObject.name == "Bullet(Clone)"){
            SceneManager.LoadScene("DestroyEnemy Scene");
        }
    }
}
