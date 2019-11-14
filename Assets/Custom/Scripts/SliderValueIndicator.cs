using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueIndicator : MonoBehaviour
{
    public Text text;
    public Slider slider;
    
    public void Start()
    {
        SetIndicator();
    }

    public void UpdateSliderValue()
    {
        SetIndicator();
    }

    private void SetIndicator()
    {
        text.text = Math.Round(slider.value, 2) + "%";
    }
}
