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
        public AlmostSquareSignal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier, 
            RectTransform rectTransform) :
            base(horizontalDisplacement, verticalDisplacement, timeBaseMultiplier)
        {
            _rectTransform = rectTransform;
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
        }
        
        public override float SignalFunction(float x)
        {
            float fx;
            var v0 = 1f;
            var R = 1000f;
            var C = 0.001f;
            x = x - _rectTransform.rect.x;
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

            return fx + verticalDisplacement;
        }
    }
}