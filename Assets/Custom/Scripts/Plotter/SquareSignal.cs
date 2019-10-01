using System;

namespace Custom.Scripts.Plotter
{
    public class SquareSignal : Signal
    {
        private float _periodFactor;
        private float _simetryFactor;
        private float _maxFx;
        private float _minFx;
        public SquareSignal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier,
            float periodFactor, float simetryFactor, float maxFx, float minFx) : base(
            horizontalDisplacement, verticalDisplacement, timeBaseMultiplier)
        {
            _periodFactor = periodFactor;
            _simetryFactor = simetryFactor;
            _minFx = minFx;
            _maxFx = maxFx;
        }

        public override float SignalFunction(float x)
        {
            var vDisplacement = acDcCoupling ? verticalDisplacement : 0f;
            return Math.Abs(Math.Floor(x / _periodFactor + horizontalDisplacement)) % _simetryFactor < 0.01
                ? _minFx + vDisplacement
                : _maxFx + vDisplacement;
        }

        public override void Reset()
        {
            
        }
    }
}