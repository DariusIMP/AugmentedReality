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


        private float _horizontalDisplacement = 0f;
        private float _verticalDisplacement = 0f;
        private float _timeBaseMultiplier = 1f;
        
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
            return (float)Math.Sin(x + _horizontalDisplacement) + _verticalDisplacement;
        }

        public void DisplaceHorizontally(float hd)
        {
            _horizontalDisplacement = hd;
            _lineRenderer.positionCount = 0;
            TestDraw2();
        }

        public void DisplaceVertically(float vd)
        {
            _verticalDisplacement = vd;
            _lineRenderer.positionCount = 0;
            TestDraw2();
        }

        public void ExpandTimeBase(float multiplier)
        {
            _timeBaseMultiplier = multiplier;
            TestDraw2();
        }
        
        public float SquareSignal(float x)
        {
            return Math.Abs(Math.Floor(x + _horizontalDisplacement)) % 2 < 0.01 ? 0 + _verticalDisplacement : 1 + _verticalDisplacement;
        }

        public void TestDraw2()
        {
            SetDots(SquareSignal);
        }
        
        public Vector2 AdjustCoordinateToCanvasSize(float x, float fx)
        {
            Rect rect = _rectTransform.rect;
            float newX = x / (_timeBaseMultiplier * maxX) * rect.x;
            float newFx = fx / maxY * rect.y;
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
