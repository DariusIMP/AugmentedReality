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
        private Signal _signal;

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
            SetSinusoidalSignal();
        }

        public void SetSquareSignal()
        {
            _signal = new SquareSignal(_horizontalDisplacement, _verticalDisplacement, _timeBaseMultiplier);
            SetDots(_signal.SignalFunction);
        }

        public void SetSinusoidalSignal()
        {
            _signal = new SinusoidalSignal(_horizontalDisplacement, _verticalDisplacement, _timeBaseMultiplier);
            SetDots(_signal.SignalFunction);
        }

        public void SetAlmostSquareSignal()
        {
            _signal = new AlmostSquareSignal(_horizontalDisplacement, _verticalDisplacement, _timeBaseMultiplier);
            SetDots(_signal.SignalFunction);
        }
        
        public void DisplaceHorizontally(float hd)
        {
            _horizontalDisplacement = hd;
            _signal.horizontalDisplacement = hd;
            _lineRenderer.positionCount = 0;
            SetDots(_signal.SignalFunction);
        }

        public void DisplaceVertically(float vd)
        {
            _verticalDisplacement = vd;
            _signal.verticalDisplacement = vd;
            _lineRenderer.positionCount = 0;
            SetDots(_signal.SignalFunction);
        }

        public void ExpandTimeBase(float multiplier)
        {
            _timeBaseMultiplier = multiplier;
            _signal.timeBaseMultiplier = multiplier;
            SetDots(_signal.SignalFunction);
        }
        
        

        private bool signalGrowing = false;
        public float AlmostSquareSignal(float t)
        {
            float v0 = 1f;
            float R = 1000f;
            float C = 0.001f;
            return Derivative(x => (float) (v0 * (1 - Math.Exp(x / (R * C)))), t) < 0.01
                ? (float) (v0 * (1 - Math.Exp((t + _horizontalDisplacement) / (R * C)))) + _verticalDisplacement
                : (float) (v0 * (1 - Math.Exp(-(t + _horizontalDisplacement) / (R * C)))) + _verticalDisplacement;
        }

        private float Derivative(Func<float, float> func, float x)
        {
            var delta = 0.01f;
            return (func(x + delta) - func(x)) / delta;
        }
        
        public void TestDraw2()
        {
            SetDots(AlmostSquareSignal);
        }
        
        public Vector2 AdjustCoordinateToCanvasSize(float x, float fx)
        {
            Rect rect = _rectTransform.rect;
            float newX = x / (_timeBaseMultiplier * maxX) * rect.x;
            float newFx = fx / maxY * rect.y;
            return new Vector2(-newX, -newFx); //TODO: chequear por qué la imagen sale invertida (el - está puesto a propósito para contrarrestar esto)
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
