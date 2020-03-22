using System;

namespace Custom.Scripts.Oscilloscope.Plotter
{
    public class SquareSignal : Signal
    {
        private float _periodFactor;
        private float _simetryFactor;
        private float _maxFx;
        private float _minFx;

        public SquareSignal(float timeBaseMultiplier, float directCurrent,
            float periodFactor, float simetryFactor, float maxFx, float minFx) : base(
            timeBaseMultiplier, directCurrent)
        {
            _periodFactor = periodFactor;
            _simetryFactor = simetryFactor;
            _minFx = minFx;
            _maxFx = maxFx;
        }

        public override float SignalFunction(float x)
        {
            var dc = acDcCoupling ? directCurrent : 0f;
            return Math.Abs(Math.Floor(x / _periodFactor)) % _simetryFactor < 0.01
                ? _minFx + dc
                : _maxFx + dc;
        }

        public override void Reset()
        {
        }
    }
}