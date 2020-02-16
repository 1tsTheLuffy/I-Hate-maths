using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeatBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void setHeatValue(float value)
    {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void setMinHeatValue(float value)
    {
        slider.minValue = value;
        slider.value = value;
        fill.color = gradient.Evaluate(0f);
    }
}
