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
        public AlmostSquareSignal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier, 
            float directCurrent,
            RectTransform rectTransform) :
            base(horizontalDisplacement, verticalDisplacement, timeBaseMultiplier, directCurrent)
        {
            _rectTransform = rectTransform;
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

            var dc = acDcCoupling ? directCurrent : 0f;
            return fx - adjustmentFactor + verticalDisplacement + dc;
        }

        private void CalculateAdjustmentFactor()
        {
            float max = (float) (v0 * (1 - Math.Exp(-(1000 - vertex) * timeBaseMultiplier / (R * C))));
            float min = (float) (v0 * (1 + Math.Exp(-(1000 - vertex) * timeBaseMultiplier / (R * C)))) - max;
            adjustmentFactor = (max - min) / 2;
        }
    }
}