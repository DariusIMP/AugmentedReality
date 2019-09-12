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
            return Math.Abs(Math.Floor(x + horizontalDisplacement)) % 2 < 0.01
                ? 0 + verticalDisplacement
                : 1 + verticalDisplacement;
        }

        public override void Reset()
        {
            
        }
    }
}