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
        const int Verticaldivs = 8;
        
        private RectTransform _rectTransform;
        
        private float _minX;//TODO: construir esto en base a las escalas de amplitud y de base de tiempo
        private float _maxX;
        private float _minY;
        private float _maxY;

        private LineRenderer _lineRenderer;
        private Signal _signal;

        private float _horizontalDisplacement = 0f;
        private float _verticalDisplacement = 0f;
        private float _timeBaseMultiplier = 1f;
        private float _directCurrent = 0f;
        
        //Unit: volts
        private readonly float[] _timeBaseScale = {0.001f, 0.002f, 0.005f, 0.01f, 0.02f,
            0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5};

        //Unit: seconds
        private readonly float[] _amplitudeScale = {0.001f, 0.002f, 0.005f, 0.01f, 0.02f,
            0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5};
        
        [Range(-100,100)]
        private float _triggerLevel = 0f;
        
        public GameObject TriggerLevelIndicator;
        public GameObject AmplitudeSlider;
        public GameObject TimeBaseSlider;
        
        public int dotsAmount;
        
        // Start is called before the first frame update
        public void Start()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            _lineRenderer.useWorldSpace = false;
            LoadScales();
            SetSinusoidalSignal();
        }

        private void LoadScales()
        {
            LoadAmplitudeScale();
            ShowAmplitudeScale();
            
            LoadTimeBaseScale();
            ShowTimeBaseScale();
        }

        private void LoadAmplitudeScale()
        {
            var ampScale = _amplitudeScale[(int)AmplitudeSlider.GetComponent<Slider>().value];
            _maxY = Verticaldivs / 2f * ampScale;
            _minY = -_maxY;
        }

        private void LoadTimeBaseScale()
        {
            var timeScale = _timeBaseScale[(int) TimeBaseSlider.GetComponent<Slider>().value];
            _timeBaseMultiplier = timeScale;
            _maxX = Horizontaldivs / 2f * timeScale;
            _minX = -_maxX;
        }

        private void ShowTimeBaseScale()
        {
            TimeBaseSlider.GetComponentInChildren<Text>().text = 
                _timeBaseScale[(int) TimeBaseSlider.GetComponent<Slider>().value] + "s";
        }
        
        private void ShowAmplitudeScale() 
        {    
            AmplitudeSlider.GetComponentInChildren<Text>().text = 
            _amplitudeScale[(int) AmplitudeSlider.GetComponent<Slider>().value] + "V";
        }
        
        public void VaryTriggerLevel(float triggerLevel)
        {
            _triggerLevel = triggerLevel;
            var triggerLevelPos = TriggerLevelIndicator.transform.localPosition;
            triggerLevelPos.y = - triggerLevel / 100 * _rectTransform.rect.y;
            TriggerLevelIndicator.transform.localPosition = triggerLevelPos;
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
            LoadTimeBaseScale();
            ShowTimeBaseScale();
            _signal.timeBaseMultiplier = _timeBaseScale[(int)index];
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void VaryAmplitude(float index)
        {
            LoadAmplitudeScale();
            ShowAmplitudeScale();
            _maxY = Verticaldivs / 2f * _amplitudeScale[(int)index];
            _minY = -_maxY;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void SetSquareSignal()
        {
            var directCurrent = 2f;
            _signal = new SquareSignal(_horizontalDisplacement, _verticalDisplacement, _timeBaseMultiplier, directCurrent,
                2f, 4f, 1f, -3f);
            SetDots(_signal.SignalFunction);
        }

        public void SetSinusoidalSignal()
        {
            var directCurrent = 0f;
            var frecuency = 1000f;
            _signal = new SinusoidalSignal(_horizontalDisplacement, _verticalDisplacement, _timeBaseMultiplier, frecuency, directCurrent);
            SetDots(_signal.SignalFunction);
        }

        public void SetAlmostSquareSignal()
        {
            var directCurrent = 1f;
            _signal = new AlmostSquareSignal(_horizontalDisplacement, _verticalDisplacement, _timeBaseMultiplier, directCurrent, _rectTransform);
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

        private Vector2 AdjustCoordinateToCanvasSize(float x, float fx)
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

            float newFx = fx / _maxY * rect.y;
            if (newFx > rect.yMax)
            {
                newFx = rect.yMax;
            } else if (newFx < rect.yMin)
            {
                newFx = rect.yMin;
            }
            
            return new Vector2(-newX, -newFx);
        }

        private void SetDots(Func<float, float> func)
        {
            _lineRenderer.positionCount = dotsAmount;
            float stepSize = (_maxX - _minX) / dotsAmount;
            float x = _minX;
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

            var triggerLocalY = TriggerLevelIndicator.transform.localPosition.y;
            return triggerLocalY < max && triggerLocalY > min;
        }
    }
}
