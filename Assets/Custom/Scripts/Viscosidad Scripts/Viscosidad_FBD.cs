using System;
using UnityEngine;

namespace Viscosidad_Scripts
{
    public class Viscosidad_FBD : MonoBehaviour
    {
        public FluxController fluxController;

        private ArrowScaler peso;

        private ArrowScaler arrastre;

        private ArrowScaler empuje;

        private ArrowScaler velocidad;

        private const float FORCES_SCALE_FACTOR = 0.28f;
        private const float VELOCITY_SCALE_FACTOR = 1.5f;

        private void Start()
        {
            peso = setupArrow("Peso", FORCES_SCALE_FACTOR);
            arrastre = setupArrow("Arrastre", FORCES_SCALE_FACTOR);
            empuje = setupArrow("Empuje", FORCES_SCALE_FACTOR);
            velocidad = setupArrow("V", VELOCITY_SCALE_FACTOR);
        }

        ArrowScaler setupArrow(string name, float scaleFactor)
        {
            ArrowScaler a = transform.Find(name).gameObject.GetComponent<ArrowScaler>();
            a.setScaleFactor(scaleFactor);
            return a;
        }

        private void Update()
        {
            var p = (float) fluxController.getGravityAccel();
            peso.resize(p);
            peso.rename("P = " + p.ToString("0.00") + " N");
            
            var a = (float) fluxController.getDragAccel();
            arrastre.resize(a);
            arrastre.rename("A = " + a.ToString("0.00") + " N");
            
            var e = (float) fluxController.getUpthrustAccel();
            empuje.resize(e);
            empuje.rename("E = " + e.ToString("0.00") + " N");

            var v = (float) fluxController.getVelocity();
            velocidad.resize(v);
            velocidad.rename("V = " + v.ToString("0.00") + " m/s");
        }
    }
}
