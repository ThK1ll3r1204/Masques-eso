using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifebar : MonoBehaviour
{

    public Slider Slider;

    public void SetMaxLife(float life)
    {
        Slider.maxValue = life;
        Slider.value = life;
    }
    public void Setlife(float life)
    {
        Slider.value = life; 
    }
}
