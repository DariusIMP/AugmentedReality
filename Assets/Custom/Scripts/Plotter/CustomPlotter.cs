using System;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.Plotter
{
    public class CustomPlotter : MonoBehaviour
    {
        const int Horizontaldivs = 10;
        const int Verticaldivs = 8;
        
        private RectTransform _rectTransform;
        
        private float _minX;
        private float _maxX;
        private float _minY;
        private float _maxY;

        private LineRenderer _lineRenderer;
        private Signal _signal;

        private float _horizontalDisplacement = 0f;
        private float _verticalDisplacement = 0f;
        private float _timeBaseMultiplier = 1f;
        private float _directCurrent = 0f;
        
        //Unit: seconds
        private readonly float[] _timeBaseScale = {0.001f, 0.002f, 0.005f, 0.01f, 0.02f,
            0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5};

        //Unit: volts
        private readonly float[] _amplitudeScale = {0.001f, 0.002f, 0.005f, 0.01f, 0.02f,
            0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5};

        private float _ampScale;
        private float _timeScale;

        public GameObject TriggerLevelIndicator;
        public GameObject AmplitudeSlider;
        public GameObject TimeBaseSlider;
        public GameObject TriggerSlider;
        public GameObject VerticalDisplacementSlider;
        public GameObject HorizontalDisplacementSlider;
        public GameObject PreciseAmplitudeSlider;

        public int dotsAmount;

        public void Start()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            _lineRenderer.useWorldSpace = false;
            
            LoadScales();
            ShowTriggerLevelVoltage();
            ShowVerticalDisplacementVoltage();
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
            _ampScale = _amplitudeScale[(int)AmplitudeSlider.GetComponent<Slider>().value];
            _maxY = Verticaldivs / 2f * _ampScale;
            _minY = -_maxY;
        }

        private void LoadTimeBaseScale()
        {
            _timeScale = _timeBaseScale[(int) TimeBaseSlider.GetComponent<Slider>().value];
            _timeBaseMultiplier = _timeScale;
            _maxX = Horizontaldivs * _timeScale;
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

        private void ShowTriggerLevelVoltage()
        {
            var triggerLevel = TriggerSlider.GetComponent<Slider>().value;
            var triggerVoltage = (float) Math.Round(triggerLevel / 100 * Verticaldivs / 2 * _ampScale, 4);
            TriggerSlider.GetComponentInChildren<Text>().text = triggerVoltage + "V";
        }

        private void ShowVerticalDisplacementVoltage()
        {
            VerticalDisplacementSlider.GetComponentInChildren<Text>().text = Math.Round(_verticalDisplacement, 4) + "V";
        }

        /**
         * Shows the percentage of horizontal displacement.
         * This method asumes simetry between the minimum and maximum value of the slider set in Unity.
         */
        private void ShowHorizontalDisplacementPercentage()
        {
            Slider slider = HorizontalDisplacementSlider.GetComponent<Slider>();
            float max = slider.maxValue;
            float x = slider.value;
            int percentage = (int) (x / max * 100);
            HorizontalDisplacementSlider.GetComponentInChildren<Text>().text = percentage + "%";
        }
        public void VaryTriggerLevel(float triggerLevel)
        {
            var triggerLevelPos = TriggerLevelIndicator.transform.localPosition;
            triggerLevelPos.y = - triggerLevel / 100 * _rectTransform.rect.y;
            TriggerLevelIndicator.transform.localPosition = triggerLevelPos;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
            ShowTriggerLevelVoltage();
        }
        
        public void VaryVerticalDisplacement(float vd)
        {
            _verticalDisplacement = vd * _ampScale;
            _lineRenderer.positionCount = 0;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
            ShowVerticalDisplacementVoltage();
        }

        public void VaryHorizontalDisplacement(float hd)
        {
            _horizontalDisplacement = hd;
            _signal.Reset();
            _lineRenderer.positionCount = 0;
            SetDots(_signal.SignalFunction);
            ShowHorizontalDisplacementPercentage();
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
            _signal.Reset();
            SetDots(_signal.SignalFunction);
            ShowTriggerLevelVoltage();
            VaryVerticalDisplacement(VerticalDisplacementSlider.GetComponent<Slider>().value);
        }

        /**
         * Varies amplitude smoothly in order to precisely adjust the amplitude to a desired scale.
         * The range of the slider should be [-0.5 , 0.5].
         */
        public void VaryAmplitudeSmoothly(float percentage)
        {
            _ampScale = _amplitudeScale[(int)AmplitudeSlider.GetComponent<Slider>().value] * (1 + percentage);
            _maxY = (Verticaldivs / 2f * _ampScale);
            _minY = -_maxY;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
            PreciseAmplitudeSlider.GetComponentInChildren<Text>().text = "x" + Math.Round(1 + percentage,2);
        }

        private void SetScales(int amplitudeIndex, int timebaseIndex)
        {
            AmplitudeSlider.GetComponent<Slider>().value = amplitudeIndex;
            TimeBaseSlider.GetComponent<Slider>().value = timebaseIndex;
            LoadScales();
        }
        
        public void SetSquareSignal()
        {
            var directCurrent = 2f;
            SetScales(1,1);
            _signal = new SquareSignal(_timeBaseMultiplier, 
                directCurrent, 0.001f, 4f, 0.002f, -3*0.002f);
            SetDots(_signal.SignalFunction);
        }

        public void SetSinusoidalSignal()
        {
            var directCurrent = 0f;
            var frecuency = 1000f;
            _signal = new SinusoidalSignal(_timeBaseMultiplier, 
                frecuency, directCurrent);
            SetScales(8,3);
            SetDots(_signal.SignalFunction);
        }

        public void SetAlmostSquareSignal()
        {
            var directCurrent = 1f;
            SetScales(9, 3);
            _signal = new AlmostSquareSignal(_verticalDisplacement, _timeBaseMultiplier, 
                directCurrent, _rectTransform);
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
            
            return new Vector2(- newX, - newFx);
        }

        private void SetDots(Func<float, float> func)
        {
            _lineRenderer.positionCount = dotsAmount;

            TriggerInfo triggerInfo = SignalIsOnTriggerLevel(func);
            if (!triggerInfo.Intersection)
            {
                _lineRenderer.positionCount = 0;
                return;
            }

            float stepSize = (_maxX - _minX) / dotsAmount;
            float x = _minX;
            float delta = triggerInfo.SignalStart - _minX;

            for (int i = 0; i < dotsAmount; i++)
            {
                var fx = func(x + delta);
                _lineRenderer.SetPosition(i, AdjustCoordinateToCanvasSize(x - _horizontalDisplacement, 
                    fx + _verticalDisplacement));
                x += stepSize;
            }
        }
    
        private class TriggerInfo
        {
            private bool _intersection;
            private float _signalStart;

            public bool Intersection
            {
                get { return _intersection; }
            }

            public float SignalStart
            {
                get { return _signalStart; }
            }

            public TriggerInfo(bool intersection, float signalStart = 0f)
            {
                _intersection = intersection;
                _signalStart = signalStart;
            }
        }
        
        private TriggerInfo SignalIsOnTriggerLevel(Func<float, float> func)
        {
            float stepSize = (_maxX - _minX) / dotsAmount;
            float x = FuncMinimum(func);
            var triggerLocalY = - TriggerLevelIndicator.transform.localPosition.y;
            var errorMargin = 5f;
            
            while (x < _maxX)
            {
                var rect = _rectTransform.rect;
                var fx = func(x) / _maxY * rect.y;
                var nfx = func(x + stepSize) / _maxY * rect.y;
                if (triggerLocalY > (fx - errorMargin) 
                    && triggerLocalY < (nfx + errorMargin)
                    || triggerLocalY < (fx + errorMargin) 
                    && triggerLocalY > (nfx - errorMargin))
                {
                    return new TriggerInfo(true, x);
                }
                x += stepSize;
            }

            return new TriggerInfo(false);
        }

        private float FuncMinimum(Func<float, float> func)
        {
            float x = _minX;
            float minX = x;
            float minFx = func(x);
            float stepSize = (_maxX - _minX) / dotsAmount;
            while (x < _maxX)
            {
                float fx = func(x);
                if (minFx > fx)
                {
                    minX = x;
                    minFx = fx;
                }
                x += stepSize;
            }
            return minX;
        }
    }
}
