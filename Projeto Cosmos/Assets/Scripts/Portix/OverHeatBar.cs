using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverHeatBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxOverHeat(float maxOverHeat)
    {
        slider.maxValue = maxOverHeat;
        slider.value = maxOverHeat;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetOverheat(float overHeat)
    {
        slider.value = overHeat;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
