using System;
using UnityEngine;

namespace Custom.Scripts.Plotter
{
    public class AlmostSquareSignal : Signal
    {
        private RectTransform _rectTransform;
        private bool signalGrowing = true;
        private float vertex = 0f;
        private float peak;
        private float v0 = 2f;
        private float R = 1000f;
        private float C = 0.001f;
        private float adjustmentFactor = 0f;
        private float _maxFx, _minFx;

        public AlmostSquareSignal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier, 
            float directCurrent,
            RectTransform rectTransform) :
            base(horizontalDisplacement, verticalDisplacement, timeBaseMultiplier, directCurrent)
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
            vertex = 0f;
            peak = 0f;
            signalGrowing = true;
            CalculateAdjustmentFactor();
        }
        
        public override float SignalFunction(float x)
        {
            float fx;
            
            x = x * 3 - _rectTransform.rect.x;
            if (signalGrowing)
            {
                fx = (float) (v0 * (1 - Math.Exp(-(x - vertex ) * timeBaseMultiplier / (R * C))));
                if (Math.Abs(Derivative(t => (float) (v0 * (1 - Math.Exp(-t * timeBaseMultiplier / (R * C)))), x - vertex)) < 0.001)
                {
                    signalGrowing = false;
                    vertex = x;
                    peak = fx;
                }
            }
            else
            {
                fx = (float) (v0 * (1 + Math.Exp(-(x - vertex) * timeBaseMultiplier / (R * C)))) - peak;
                if (Math.Abs(Derivative(t => (float) (v0 * (1 + Math.Exp(-t * timeBaseMultiplier / (R * C)))), x - vertex)) < 0.001)
                {
                    signalGrowing = true;
                    vertex = x;
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
            return fx - adjustmentFactor + verticalDisplacement + dc;
        }

        private void CalculateAdjustmentFactor()
        {
            adjustmentFactor = (_maxFx - _minFx) / 2;
        }

        private void CalculateExtremes()
        {
            _maxFx = (float) (v0 * (1 - Math.Exp(-(1000 - vertex) * timeBaseMultiplier / (R * C))));
            _minFx = (float) (v0 * (1 + Math.Exp(-(1000 - vertex) * timeBaseMultiplier / (R * C)))) - _maxFx;
            Debug.Log("MinFx: " + _minFx + " -- MaxFx: " + _maxFx);
        }
    }
}