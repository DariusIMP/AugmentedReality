using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.Plotter
{
    public class CustomPlotter : MonoBehaviour
    {
        const int Horizontaldivs = 10;
        const int Verticaldivs = 10;

        private Image PlotImage;

        public List<Vector2> _dots;

        public int dotsAmount;
        
        // Start is called before the first frame update
        public void Start()
        {
            //PlotImage = GetComponent<Image>();
            _dots = new List<Vector2>();
            Debug.Log("AAAAAAAA");
            Test();
        }

        public void SetDots(float minX, float maxX, Func<float, float> func)
        {
            float stepSize = (maxX - minX) / dotsAmount;
            float x = minX;
            for (int i = 0; i < dotsAmount; i++)
            {
                var fx = func(x);
                _dots.Add(new Vector2(x, fx));
                x += stepSize;
            }
        }

        public float Xsquare(float x)
        {
            return x * x;
        }

        public void Test()
        {
            Debug.Log("YYY");
            SetDots(-10, 10, Xsquare);
            for (int i = 0; i < _dots.Count; i++)
            {
                Debug.Log("Vector : " + _dots[i].x + "-" + _dots[i].y);
            }

            Debug.Log("ZZZ");
        }
        
        
    }
}
