using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{

    public string Unit;
    public float Factor = 1;


    // Start is called before the first frame update
    void Start()
    {
        UpdateValue();
    }

    public void UpdateValue()
    {
        double value = gameObject.GetComponentInParent<Slider>().value;
        gameObject.GetComponent<Text>().text = string.Format("{0:0.00}{1}", value * Factor, Unit);
    }

}
