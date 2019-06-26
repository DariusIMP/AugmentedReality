using System;
using UnityEngine;

namespace Custom.Scripts.CampoRotanteScripts
{
    public class ArrowOscilator : CampoRotanteArrowController
    {
        private static float MIN_SCALE = 10E-10f;

        public float timeFactor = 1f;
        public double desfasaje;

        void Start()
        {
            base.Start();
            setDesfasaje(desfasaje);
        }

        public void setDesfasaje(double desfasaje)
        {
            this.desfasaje = desfasaje * Math.PI / 180;
        }
        
        public void setScaleFactor(float factor)
        {
            scaleFactor = factor;
        }
        
        private void Update()
        {
            float l = length  * (float)Math.Sin(Time.realtimeSinceStartup * timeFactor - desfasaje);
            head.localPosition = new Vector3(l, 0, 0);
            body.localScale = new Vector3(-l, body.localScale.y, body.localScale.z);
            if (l < 0 && !inverted)
            {
                inverted = true;
                head.transform.Rotate(0, 180, 0);
            }
            if (l >= 0 && inverted)
            {
                inverted = false;
                head.transform.Rotate(0, 180, 0);
            }
        }
    }
}