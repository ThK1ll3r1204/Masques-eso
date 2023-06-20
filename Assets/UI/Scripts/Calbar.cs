using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calbar : MonoBehaviour
{
    public Slider Slider;

    public void SetMaxCalcio(float cal)
    {
        Slider.maxValue = cal;
        Slider.value = cal;
    }
    public void SetCalcio(float calc)
    {
        Slider.value = calc;
    }
}
