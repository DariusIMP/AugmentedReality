using System;

namespace Custom.Scripts.Plotter
{
    public abstract class Signal
    {
        
        public float horizontalDisplacement = 0f;
        
        public float verticalDisplacement = 0f;
        
        public float timeBaseMultiplier = 1f;

        protected Signal(float horizontalDisplacement, float verticalDisplacement, float timeBaseMultiplier)
        {
            this.horizontalDisplacement = horizontalDisplacement;
            this.verticalDisplacement = verticalDisplacement;
            this.timeBaseMultiplier = timeBaseMultiplier;
        }

        public abstract float SignalFunction(float x);

        public abstract void Reset();
    }
}