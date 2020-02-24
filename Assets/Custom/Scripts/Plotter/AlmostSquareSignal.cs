using System;
using UnityEngine;

namespace Custom.Scripts.Plotter
{
    public class AlmostSquareSignal : Signal
    {
        private static int FRECUENCY_FACTOR = 3;
        private static double DERIVATIVE_THRESHOLD = 0.0005;
        private static float EXTREMES_PREIMAGE = 1000f;
        private static float v0 = 4f;
        private static float R = 0.001f;
        private static float C = 0.025f;
        
        private RectTransform _rectTransform;
        private bool _signalGrowing = true;
        private float _vertex = 0f;
        private float _peak;
        private float _adjustmentFactor = 0f;
        private float _maxFx;
        private float _minFx;

        public AlmostSquareSignal(float verticalDisplacement, float timeBaseMultiplier, 
            float directCurrent,
            RectTransform rectTransform) :
            base(timeBaseMultiplier, directCurrent)
        {
            _rectTransform = rectTransform;
            CalculateExtremes();
            CalculateAdjustmentFactor();
        }

        private float Derivative(Func<float, float> func, float x)
        {
            var delta = 0.01f;
            return (func(x + delta) - func(x)) / delta;
        }

        public override void Reset()
        {
            _vertex = 0f;
            _peak = 0f;
            _signalGrowing = true;
        }

        private double GrowingFunction(float t)
        {
            return v0 * (1 - Math.Exp(-Math.Abs(t) * timeBaseMultiplier / (R * C)));
        }

        private double DecreasingFunction(float t)
        {
            return v0 * (1 + Math.Exp(-Math.Abs(t) * timeBaseMultiplier / (R * C)));
        }
        
        public override float SignalFunction(float x)
        {
            float fx;
            x *= FRECUENCY_FACTOR;
            if (_signalGrowing)
            {
                fx = (float) GrowingFunction(x - _vertex);
                if (Math.Abs(Derivative(t => (float) GrowingFunction(t), x - _vertex)) < DERIVATIVE_THRESHOLD)
                {
                    _signalGrowing = false;
                    _vertex = x;
                    _peak = fx;
                }
            }
            else
            {
                fx = (float) DecreasingFunction(x - _vertex) - _peak;
                if (Math.Abs(
                        Derivative(t => (float) DecreasingFunction(t), x - _vertex)) < DERIVATIVE_THRESHOLD)
                {
                    _signalGrowing = true;
                    _vertex = x;
                }
            }

            if (fx > _maxFx)
            {
                fx = _maxFx;
            }

            if (fx < _minFx)
            {
                fx = _minFx;
            }

            var dc = acDcCoupling ? directCurrent : 0f;
            return fx - _adjustmentFactor + dc;
        }

        private void CalculateAdjustmentFactor()
        {
            _adjustmentFactor = (_maxFx - _minFx) / 2;
        }

        /**
         * Calculates the extremes of the image of the function being used as signal (that is the maximum and minimum
         * values of the function), using EXTREMES_PREIMAGE as the value for evaluation. The reason it's used like this
         * is that the functions are continuously increasing (or continuously decreasing) and converging into
         * asymptotes. Using EXTREMES_PREIMAGE value for evaluation of the functions is a good enough approach to get
         * the maximum and minimum values of the functions, which will be very close to the value of these horizontal
         * asymptotes.  
         */
        private void CalculateExtremes()
        {
            _maxFx = (float) GrowingFunction(EXTREMES_PREIMAGE);
            _minFx = (float) DecreasingFunction(EXTREMES_PREIMAGE) - _maxFx;
        }
    }
}