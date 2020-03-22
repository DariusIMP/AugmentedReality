using System;

namespace Custom.Scripts.Oscilloscope.Plotter
{
    public abstract class Signal
    {
        public float timeBaseMultiplier = 1f;
        public float directCurrent = 0f;
        public Boolean acDcCoupling = false;

        protected Signal(float timeBaseMultiplier, float directCurrent)
        {
            this.timeBaseMultiplier = timeBaseMultiplier;
            this.directCurrent = directCurrent;
        }

        public abstract float SignalFunction(float x);

        public abstract void Reset();

        public void ToggleAcDcCoupling()
        {
            if (!acDcCoupling)
            {
                acDcCoupling = true;
            } else
            {
                acDcCoupling = false;
            }
        }
    }
}