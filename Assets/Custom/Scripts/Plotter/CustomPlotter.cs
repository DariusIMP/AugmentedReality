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
        private float PlotWidth;
        private float PlotHeight;
        
        public List<Vector2> _dots;
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
        
        public int dotsAmount;
        
        // Start is called before the first frame update
        public void Start()
        {
            PlotImage = GetComponent<Image>();
            PlotWidth = PlotImage.rectTransform.rect.width;
            PlotHeight = PlotImage.rectTransform.rect.height;
            
            if (PlotImage == null) throw new Exception("Simplest plot needs an image component in the same GameObject in order to work.");
            _dots = new List<Vector2>();
            Test();
        }

        public Vector2 AdjustCoordinateToImageSize(float x, float fx)
        {
            float newX = x / maxX * (PlotWidth / 2);
            float newFx = fx / maxY * (PlotHeight / 2);
            return new Vector2(newX, newFx);
        }
        
        public void SetDots(Func<float, float> func)
        {
            float stepSize = (maxX - minX) / dotsAmount;
            float x = minX;
            for (int i = 0; i <= dotsAmount; i++)
            {
                var fx = func(x);
                _dots.Add(AdjustCoordinateToImageSize(x, fx));
                x += stepSize;
            }
        }

        public float Xsquare(float x)
        {
            return x * x;
        }

        public void Test()
        {
            minX = -10;
            maxX = 10;
            
            SetDots(Xsquare);
            for (int i = 0; i < _dots.Count; i++)
            {
                Debug.Log("Vector : " + _dots[i].x + "-" + _dots[i].y);
            }
        }
        
        
    }
}
