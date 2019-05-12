
using System;
using UnityEngine;
using UnityEngine.UI;


public class ArrowOscilator : MonoBehaviour
{
    private static float MIN_SCALE = 10E-10f;

    // Length of the arrow.
    public float length = 1f;
    // Scale of the length of the arrow.
    public float scaleFactor = 0.1f;
    public float timeFactor = 1f;
    public double desfasaje = 0;


    private Transform head;
    private Transform body;
    private bool inverted;

    void Start()
    {
        head = gameObject.transform.Find("Head");
        body = gameObject.transform.Find("Body");
        inverted = length < 0;
        resize(length);
        setDesfasaje(desfasaje);
    }

    public void setDesfasaje(double desfasaje)
    {
        this.desfasaje = desfasaje * Math.PI / 180;
    }

    public void resize(float length)
    {
        float l = Math.Abs(length) * scaleFactor;
        gameObject.SetActive(true);
        head.localPosition = new Vector3(l, 0, 0);
        body.localScale = new Vector3(-l, body.localScale.y, body.localScale.z);
    }

    public void setScaleFactor(float factor)
    {
        scaleFactor = factor;
    }

    private void Update()
    {
        float l = length  * (float)Math.Sin(Time.realtimeSinceStartup * timeFactor - desfasaje);
        head.localPosition = new Vector3(l, 0, 0);
        body.localScale = new Vector3(-l, body.localScale.y, body.localScale.z);
        if (l < 0 && !inverted)
        {
            inverted = true;
            head.transform.Rotate(0, 180, 0);
        }
        if (l >= 0 && inverted)
        {
            inverted = false;
            head.transform.Rotate(0, 180, 0);
        }
    }
}