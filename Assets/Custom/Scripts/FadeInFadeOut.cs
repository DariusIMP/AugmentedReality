using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FadeInFadeOut : MonoBehaviour
{
    private Light _light;

    private bool crescendo;

    public float maxIntensity;

    public float minIntensity;

    public float multiplier;
    
    // Start is called before the first frame update
    void Start()
    {
        _light = gameObject.GetComponent<Light>();
        _light.enabled = false;
    }

    public void OnPointerDown ()
    {
        _light.enabled = true;
    }

    public void OnPointerUp()
    {
        _light.enabled = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (crescendo)
        {
            _light.intensity += Time.deltaTime * multiplier;
            crescendo = _light.intensity < maxIntensity;
        }
        else
        {
            _light.intensity -= Time.deltaTime * multiplier;
            crescendo = _light.intensity < minIntensity;
        }
    }
}
