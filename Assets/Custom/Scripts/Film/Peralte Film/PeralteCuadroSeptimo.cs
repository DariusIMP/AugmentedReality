using UnityEngine;

namespace Film.Peralte_Film
{
    public class PeralteCuadroSeptimo : CuadroPeralte {

        /**
     * En este cuadro segundo para la prueba se muestra:
     * - el auto andando sobre la ruta circular en modo lento
     */

		public override void Setup (){}

        public override void Play()
        {
            Debug.Log(name + ".Play()");
            base.Play();
            DialogueManager.Play();
        }

        public override void Stop()
        {
            Debug.Log("<color=blue> PeralteCuadroSegundo.stop() </color>");
            base.Stop();
        }    
        
        protected override void Start()
        {
            base.Start();
            Debug.Log(name + ": Start");
        }
    }
}
