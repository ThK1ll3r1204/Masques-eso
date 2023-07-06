using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calbar : MonoBehaviour
{
    PlayerStats pStats;
    public Slider Slider;



    private void Awake()
    {
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    private void FixedUpdate()
    {
        Slider.value = pStats._calcio;
    }

}
