using System;

namespace Custom.Scripts.Plotter
{
    public class SinusoidalSignal : Signal
    {
        public SinusoidalSignal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier) :
            base(horizontalDisplacement, verticalDisplacement, timeBaseMultiplier)
        {
        }

        public override float SignalFunction(float x)
        {
            var vDisplacement = acDcCoupling ? verticalDisplacement : 0f;
            return (float) Math.Sin(x + horizontalDisplacement) + vDisplacement;
        }

        public override void Reset()
        {
            
        }
    }
}