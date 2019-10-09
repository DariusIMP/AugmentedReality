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
        
        //Unit: volts
        private readonly float[] _timeBaseScale = {0.001f, 0.002f, 0.005f, 0.01f, 0.02f,
            0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5};

        //Unit: seconds
        private readonly float[] _amplitudeScale = {0.001f, 0.002f, 0.005f, 0.01f, 0.02f,
            0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5};
        
        [Range(-100,100)]
        private float _triggerLevel = 0f;
        
        public GameObject triggerLevelIndicator;
        
        public int dotsAmount;
        
        // Start is called before the first frame update
        public void Start()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            _lineRenderer.useWorldSpace = false;

            SetSinusoidalSignal();
        }

        public void VaryTriggerLevel(float triggerLevel)
        {
            _triggerLevel = triggerLevel;
            Vector3 triggerLevelPos = triggerLevelIndicator.transform.localPosition;
            triggerLevelPos.y = triggerLevel / 100 * _rectTransform.rect.y;
            triggerLevelIndicator.transform.localPosition = triggerLevelPos;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }
        
        public void ToggleAcDcCoupling()
        {
            _signal.ToggleAcDcCoupling();
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void ExpandTimeBase(float index)
        {
            _signal.timeBaseMultiplier = _timeBaseScale[(int)index];
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void VaryAmplitude(float index)
        {
            maxY = Verticaldivs / 2f * _amplitudeScale[(int)index];
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


        public Vector2 AdjustCoordinateToCanvasSize(float x, float fx)
        {
            Rect rect = _rectTransform.rect;
            float newX = x / (_signal.timeBaseMultiplier * (Horizontaldivs / 2f)) * rect.x;
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

            if (!SignalIsOnTriggerLevel())
            {
                _lineRenderer.positionCount = 0;
            }
        }

        private bool SignalIsOnTriggerLevel()
        {
            var positions = new Vector3[_lineRenderer.positionCount];
            _lineRenderer.GetPositions(positions);
            var max = positions[0].y;
            var min = positions[0].y;
            foreach (var position in positions)
            {
                if (position.y > max)
                {
                    max = position.y;
                }

                if (position.y < min)
                {
                    min = position.y;
                }
            }

            var triggerLocalY = triggerLevelIndicator.transform.localPosition.y;
            return triggerLocalY < max && triggerLocalY > min;
        }
    }
}
