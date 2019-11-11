using System;

namespace Custom.Scripts.Plotter
{
    public abstract class Signal
    {
        
        public float horizontalDisplacement = 0f;
        
        public float verticalDisplacement = 0f;
        
        public float timeBaseMultiplier = 1f;

        public float directCurrent = 0f;
        
        public Boolean acDcCoupling = false;

        protected Signal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier, float directCurrent)
        {
            this.horizontalDisplacement = horizontalDisplacement;
            this.verticalDisplacement = verticalDisplacement;
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