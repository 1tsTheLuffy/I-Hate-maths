using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetValue(int value)
    {
        slider.value = value;
    }

    public void SetMinValue(int value)
    {
        slider.minValue = value;
        slider.value = value;
    }
}