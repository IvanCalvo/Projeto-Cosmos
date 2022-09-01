using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    //declaracao das texturas do cursor caso tenha mais de uma etc
    public Texture2D cursor;
    public Texture2D cursorHUD;
    public Texture2D cursorAtirando;

    private void Awake()
    {
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width/2 , cursorType.height/2);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}