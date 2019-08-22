using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{

    public string Unit;
    public float Factor = 1;
    public int Decimals = 2;


    // Start is called before the first frame update
    void Start()
    {
        UpdateValue();
    }

    public void UpdateValue()
    {
        double value = gameObject.GetComponentInParent<Slider>().value;
        string format = "{0:0." + new string('0', Decimals) + "}{1}";
        gameObject.GetComponent<Text>().text = string.Format(format, value * Factor, Unit);
    }

}
