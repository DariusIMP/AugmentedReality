using System;
using Custom.Scripts.Oscilloscope.Sliders;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.Oscilloscope.Plotter
{
    public class CustomPlotter : MonoBehaviour
    {
        const int Horizontaldivs = 10;
        const int Verticaldivs = 8;
        
        private RectTransform _rectTransform;
        
        private float _minX;
        private float _maxX;
        private float _maxY;

        private LineRenderer _lineRenderer;
        private Signal _signal;

        private float _horizontalDisplacement = 0f;
        private float _verticalDisplacement = 0f;
        private float _timeBaseMultiplier = 1f;
        private float _directCurrent = 0f;
        private float _triggerLevel = 0f;
        private float _smoothVerticalScale = 1f;

        public PlotManager PlotManager;
        
        //Unit: seconds
        public static float[] TIME_BASE_SCALE = {0.001f, 0.002f, 0.005f, 0.01f, 0.02f,
            0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5};

        //Unit: volts
        public static float[] VERTICAL_SCALE = {0.001f, 0.002f, 0.005f, 0.01f, 0.02f,
            0.05f, 0.1f, 0.2f, 0.5f, 1, 2, 5};

        private static float DEFAULT_VERTICAL_SCALE = VERTICAL_SCALE[8];
        private static float DEFAULT_TIME_BASE_SCALE = TIME_BASE_SCALE[4];

        private float _verticalScale =  VERTICAL_SCALE[4];
        private float _timeScale = TIME_BASE_SCALE[8];

        public GameObject TriggerLevelIndicator;

        public int dotsAmount;

        public void Start()
        {
            _rectTransform = gameObject.GetComponent<RectTransform>();
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
            _lineRenderer.useWorldSpace = false;
            
            LoadDefaultScales();
            SetSinusoidalSignal();
        }

        public float GetVerticalScale()
        {
            return _verticalScale;
        }

        public float GetTimeBaseScale()
        {
            return _timeScale;
        }
        
        private void LoadDefaultScales()
        {
            LoadVerticalScale(DEFAULT_VERTICAL_SCALE);
            LoadTimeBaseScale(DEFAULT_TIME_BASE_SCALE);
        }

        private void LoadVerticalScale(float verticalScale)
        {
            _verticalScale = verticalScale;
            _maxY = Verticaldivs / 2f * _verticalScale;
        }

        private void LoadTimeBaseScale(float timeBaseScale)
        {
            _timeScale = timeBaseScale;
            _timeBaseMultiplier = _timeScale;
            _maxX = Horizontaldivs * _timeScale;
            _minX = -_maxX;
        }

        public float GetTriggerLevelVoltage()
        {
            return (float) Math.Round(_triggerLevel / 100 * Verticaldivs / 2 * _verticalScale, 4);
        }

        public float GetVerticalDisplacement()
        {
            return (float) Math.Round(_verticalDisplacement, 4);
        }

        public void VaryTriggerLevel(float triggerLevel)
        {
            _triggerLevel = triggerLevel;
            var triggerLevelPos = TriggerLevelIndicator.transform.localPosition;
            triggerLevelPos.y = - _triggerLevel / 100 * _rectTransform.rect.y;
            TriggerLevelIndicator.transform.localPosition = triggerLevelPos;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }
        
        public void VaryVerticalDisplacement(float vd)
        {
            _verticalDisplacement = vd * _verticalScale;
            _lineRenderer.positionCount = 0;
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void VaryHorizontalDisplacement(float hd)
        {
            _horizontalDisplacement = hd;
            _signal.Reset();
            _lineRenderer.positionCount = 0;
            SetDots(_signal.SignalFunction);
        }

        public float GetHorizontalDisplacement()
        {
            return _horizontalDisplacement;
        }
        
        public void ToggleAcDcCoupling()
        {
            _signal.ToggleAcDcCoupling();
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void SetTimeBaseScale(float index)
        {
            LoadTimeBaseScale(TIME_BASE_SCALE[(int) index]);
            _signal.timeBaseMultiplier = TIME_BASE_SCALE[(int) index];
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public void VaryVerticalScale(float index)
        {
            LoadVerticalScale(VERTICAL_SCALE[(int) index]);
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        /**
         * Varies amplitude smoothly in order to precisely adjust the amplitude to a desired scale.
         * The range of the slider should be [-0.5 , 0.5].
         */
        public void VaryVerticalScaleSmoothly(float percentage)
        {
            _smoothVerticalScale = (1 + percentage); 
            float preciseVerticalScale = _verticalScale * _smoothVerticalScale;
            _maxY = (Verticaldivs / 2f * preciseVerticalScale);
            _signal.Reset();
            SetDots(_signal.SignalFunction);
        }

        public float GetSmoothVerticalScale()
        {
            return _smoothVerticalScale;
        }

        private void SetScales(int amplitudeIndex, int timebaseIndex)
        {
            _verticalScale = VERTICAL_SCALE[amplitudeIndex];
            _timeScale = TIME_BASE_SCALE[timebaseIndex];
            PlotManager.BroadcastVerticalScaleVariation(amplitudeIndex);
            PlotManager.BroadcastTimeBaseScaleVariation(timebaseIndex);
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
