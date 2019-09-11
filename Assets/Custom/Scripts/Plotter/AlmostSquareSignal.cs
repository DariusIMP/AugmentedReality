using System;

namespace Custom.Scripts.Plotter
{
    public class AlmostSquareSignal : Signal
    {
        private bool signalGrowing = true;

        public AlmostSquareSignal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier) :
            base(horizontalDisplacement, verticalDisplacement, timeBaseMultiplier)
        {
        }

        public float AlmostSquareSignalFunc(float t)
        {
            var v0 = 1f;
            var R = 1000f;
            var C = 0.001f;
            return Derivative(x => (float) (v0 * (1 - Math.Exp(x / (R * C)))), t) < 0.01
                ? (float) (v0 * (1 - Math.Exp((t + horizontalDisplacement) / (R * C)))) + verticalDisplacement
                : (float) (v0 * (1 - Math.Exp(-(t + horizontalDisplacement) / (R * C)))) + verticalDisplacement;
        }

        private float Derivative(Func<float, float> func, float x)
        {
            var delta = 0.01f;
            return (func(x + delta) - func(x)) / delta;
        }


        public override float SignalFunction(float x)
        {
            throw new NotImplementedException();
        }
    }
}