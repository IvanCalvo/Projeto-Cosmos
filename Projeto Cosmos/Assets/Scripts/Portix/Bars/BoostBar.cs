using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxBoost(float maxBoost)
    {
        slider.maxValue = maxBoost;
        //slider.value = maxBoost;
    }

    public void SetBoost(float boost)
    {
        slider.value = Mathf.MoveTowards(slider.value, boost, 300f * Time.deltaTime);
    }
}
