using System;

namespace Custom.Scripts.Plotter
{
    public class SinusoidalSignal : Signal
    {
        public SinusoidalSignal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier, 
            float directCurrent) : base(horizontalDisplacement, verticalDisplacement, timeBaseMultiplier, directCurrent)
        {
        }

        public override float SignalFunction(float x)
        {
            var dc = acDcCoupling ? directCurrent : 0f;
            return (float) Math.Sin(x + horizontalDisplacement) + verticalDisplacement + dc;
        }

        public override void Reset()
        {
            
        }
    }
}