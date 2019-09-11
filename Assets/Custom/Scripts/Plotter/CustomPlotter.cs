using System;
using System.Collections.Generic;
using CommandInterpreter.Calculator.Container.Functions;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.Plotter
{
    public class CustomPlotter : MonoBehaviour
    {
        const int Horizontaldivs = 10;
        const int Verticaldivs = 10;

//        private Image PlotImage;
        private RectTransform _rectTransform;

//        private Texture2D PlotTexture;
//        private Vector2 TextureResolution = new Vector2(500, 500);

        public float minX;
        public float maxX;
        public float minY;
        public float maxY;

        private LineRenderer _lineRenderer;
        
        float horizontalDisplacement = 0f;
        float verticalDisplacement = 0f;
        
        public int dotsAmount;
        
        // Start is called before the first frame update
        public void Start()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            _lineRenderer.useWorldSpace = false;
            TestDraw2();
        }

        public float Sin(float x)
        {
            return (float)Math.Sin(x + horizontalDisplacement) + verticalDisplacement;
        }

        public void DisplaceHorizontally(float hd)
        {
            horizontalDisplacement = hd;
            _lineRenderer.positionCount = 0;
            TestDraw2();
        }

        public void DisplaceVertically(float vd)
        {
            verticalDisplacement = vd;
            _lineRenderer.positionCount = 0;
            TestDraw2();
        }
        
        public float SquareSignal(float x)
        {
            return Math.Abs(Math.Floor(x)) % 2 < 0.01 ? 0 : 1;
        }

        public void TestDraw2()
        {
            SetDots(Sin);
        }
        
        public Vector2 AdjustCoordinateToCanvasSize(float x, float fx)
        {
            float newX = x / maxX * (_rectTransform.rect.x);
            float newFx = fx / maxY * (_rectTransform.rect.y);
            return new Vector2(newX, newFx);
        }
        
        public void SetDots(Func<float, float> func)
        {
            _lineRenderer.positionCount = dotsAmount;
            float stepSize = (maxX - minX) / dotsAmount;
            float x = minX;
            for (int i = 0; i < dotsAmount; i++)
            {
                var fx = func(x);
                _lineRenderer.SetPosition(i, AdjustCoordinateToCanvasSize(x, fx));
                x += stepSize;
            }
        }
    }
}
