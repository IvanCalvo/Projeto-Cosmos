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
        //slider.value = overHeat;
        slider.value = Mathf.MoveTowards(slider.value, overHeat, 10f * Time.deltaTime);
        //Mathf.MoveTowards(currentValue, value, changeSpeed * Time.deltaTime);

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
