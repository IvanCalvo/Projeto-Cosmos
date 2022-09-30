using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintsOnButton : MonoBehaviour
{
    public GameObject hintText;
    public void OnMouseOver()
    {
        hintText.SetActive(true);
    }
    public void OnMouseExit()
    {
        hintText.SetActive(false);
    }
}
