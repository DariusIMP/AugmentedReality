using System;

namespace Custom.Scripts.Plotter
{
    public class SquareSignal : Signal
    {
        public SquareSignal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier) : base(
            horizontalDisplacement, verticalDisplacement, timeBaseMultiplier)
        {
        }

        public override float SignalFunction(float x)
        {
            var vDisplacement = acDcCoupling ? verticalDisplacement : 0f;
            return Math.Abs(Math.Floor(x + horizontalDisplacement)) % 2 < 0.01
                ? 0 + vDisplacement
                : 1 + vDisplacement;
        }

        public override void Reset()
        {
            
        }
    }
}