using System;
using UnityEngine;

namespace Custom.Scripts.Plotter
{
    public class AlmostSquareSignal : Signal
    {
        private RectTransform _rectTransform;
        private bool _signalGrowing = true;
        private float _vertex = 0f;
        private float _peak;
        private float v0 = 4f;
        private float R = 0.001f;
        private float C = 0.025f;
        private float _adjustmentFactor = 0f;
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
            _vertex = 0f;
            _peak = 0f;
            _signalGrowing = true;
            CalculateAdjustmentFactor();
        }
        
        public override float SignalFunction(float x)
        {
            float fx;
            
            x = x * 3 - _rectTransform.rect.x;
            if (_signalGrowing)
            {
                fx = (float) (v0 * (1 - Math.Exp(-(x - _vertex ) * timeBaseMultiplier / (R * C))));
                if (Math.Abs(Derivative(t => (float) (v0 * (1 - Math.Exp(-t * timeBaseMultiplier / (R * C)))), x - _vertex)) < 0.0005)
                {
                    _signalGrowing = false;
                    _vertex = x;
                    _peak = fx;
                }
            }
            else
            {
                fx = (float) (v0 * (1 + Math.Exp(-(x - _vertex) * timeBaseMultiplier / (R * C)))) - _peak;
                if (Math.Abs(Derivative(t => (float) (v0 * (1 + Math.Exp(-t * timeBaseMultiplier / (R * C)))), x - _vertex)) < 0.0005)
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
            return fx - _adjustmentFactor + verticalDisplacement + dc;
        }

        private void CalculateAdjustmentFactor()
        {
            _adjustmentFactor = (_maxFx - _minFx) / 2;
        }

        private void CalculateExtremes()
        {
            _maxFx = (float) (v0 * (1 - Math.Exp(-(1000 - _vertex) * timeBaseMultiplier / (R * C))));
            _minFx = (float) (v0 * (1 + Math.Exp(-(1000 - _vertex) * timeBaseMultiplier / (R * C)))) - _maxFx;
        }
    }
}