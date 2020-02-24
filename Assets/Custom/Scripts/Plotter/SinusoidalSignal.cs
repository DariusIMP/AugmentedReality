using System;
using UnityEngine;

namespace Custom.Scripts.Plotter
{
    public class SinusoidalSignal : Signal
    {
        private float _frecuency;
        public SinusoidalSignal(float timeBaseMultiplier, float frecuency,
            float directCurrent) : base(timeBaseMultiplier, directCurrent)
        {
            _frecuency = frecuency;
        }

        public override float SignalFunction(float x)
        {
            var dc = acDcCoupling ? directCurrent : 0f;
            return (float) Math.Sin(_frecuency * x) + dc;
        }

        public override void Reset() {}
    }
}