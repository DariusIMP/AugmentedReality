using System;

namespace Custom.Scripts.Plotter
{
    public class SinusoidalSignal : Signal
    {
        private float _frecuency;
        public SinusoidalSignal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier, float frecuency,
            float directCurrent) : base(horizontalDisplacement, verticalDisplacement, timeBaseMultiplier, directCurrent)
        {
            _frecuency = frecuency;
        }

        public override float SignalFunction(float x)
        {
            var dc = acDcCoupling ? directCurrent : 0f;
            return (float) Math.Sin(_frecuency * x + horizontalDisplacement) + verticalDisplacement + dc;
        }

        public override void Reset()
        {
            
        }
    }
}