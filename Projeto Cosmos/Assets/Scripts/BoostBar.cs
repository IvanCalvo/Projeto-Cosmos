using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoostBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxBoost(float maxValue)
    {
        slider.maxValue = maxValue;
        slider.value = maxValue;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetBoost(float value)
    {
        slider.value = Mathf.MoveTowards(slider.value, value, 30f * Time.deltaTime);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
