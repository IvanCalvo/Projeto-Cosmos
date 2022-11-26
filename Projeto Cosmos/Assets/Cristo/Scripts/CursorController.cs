using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    //declaracao das texturas do cursor caso tenha mais de uma etc
    public Texture2D cursor;
    public Texture2D currentCursor;
    public Texture2D cursorHUD;
    public Texture2D cursorAtirando;
    public Texture2D cursorHitMarker;

    private void Awake()
    {
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ChangeCursor(Texture2D cursorType)
    {
        if(cursorType != cursorHitMarker)
            currentCursor = cursorType;
        Vector2 hotspot = new Vector2(cursorType.width/2 , cursorType.height/2);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.ForceSoftware);
    }

    public void HitMarker()
    {
        Texture2D beforeCursor = currentCursor;
        ChangeCursor(cursorHitMarker);
        StartCoroutine(HitMarkerTimer());
    }


    IEnumerator HitMarkerTimer()
    {
        yield return new WaitForSeconds(0.4f);
        ChangeCursor(currentCursor);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

}