using System;
using System.Collections.Generic;
using CommandInterpreter.Calculator.Container.Functions;
using UnityEngine;
using UnityEngine.UI;
using Custom.Scripts.Plotter;

namespace Custom.Scripts.Plotter
{
    public class CustomPlotter : MonoBehaviour
    {
        const int Horizontaldivs = 10;
        const int Verticaldivs = 10;
        
        private RectTransform _rectTransform;
        
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

        public void ToggleAcDcCoupling()
        {
            _signal.ToggleAcDcCoupling();
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void VaryAmplitude(float factor)
        {
            maxY = Verticaldivs / (2f * factor);
            minY = -maxY;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }
        
        public void SetSquareSignal()
        {
            _signal = new SquareSignal(_horizontalDisplacement, _verticalDisplacement, _timeBaseMultiplier, 
                2f, 4f, 1f, -3f);
            SetDots(_signal.SignalFunction);
        }

        public void SetSinusoidalSignal()
        {
            _signal = new SinusoidalSignal(_horizontalDisplacement, _verticalDisplacement, _timeBaseMultiplier);
            SetDots(_signal.SignalFunction);
        }

        public void SetAlmostSquareSignal()
        {
            _signal = new AlmostSquareSignal(_horizontalDisplacement, _verticalDisplacement, _timeBaseMultiplier, _rectTransform);
            SetDots(_signal.SignalFunction);
        }
        
        public void DisplaceHorizontally(float hd)
        {
            _horizontalDisplacement = hd;
            _signal.horizontalDisplacement = hd;
            _lineRenderer.positionCount = 0;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void DisplaceVertically(float vd)
        {
            _verticalDisplacement = vd;
            _signal.verticalDisplacement = vd;
            _lineRenderer.positionCount = 0;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void ExpandTimeBase(float multiplier)
        {
            _timeBaseMultiplier = multiplier;
            _signal.timeBaseMultiplier = multiplier;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public Vector2 AdjustCoordinateToCanvasSize(float x, float fx)
        {
            Rect rect = _rectTransform.rect;
            float newX = x / (_timeBaseMultiplier * maxX) * rect.x;
            if (newX > rect.xMax)
            {
                newX = rect.xMax;
            } else if (newX < rect.xMin)
            { 
                newX = rect.xMin;
            }

            float newFx = fx / maxY * rect.y;
            if (newFx > rect.yMax)
            {
                newFx = rect.yMax;
            } else if (newFx < rect.yMin)
            {
                newFx = rect.yMin;
            }
            
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
