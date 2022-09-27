using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxShield(float maxShield)
    {
        slider.maxValue = maxShield;
        //slider.value = maxShield;
        SetShield(maxShield);
        //fill.color = gradient.Evaluate(1f);
    }

    public void SetShield(float shield)
    {
        slider.value = Mathf.MoveTowards(slider.value, shield, 30f * Time.deltaTime);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
