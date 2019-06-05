using UnityEngine;

namespace Film.Peralte_Film
{
    public class PeralteCuadroFinal : CuadroPeralte {

        /**
     * En este cuadro segundo para la prueba se muestra:
     * - el auto andando sobre la ruta circular en modo lento
     */
		public override void Setup() {}

        public override void Play()
        {
            Debug.Log("<color=blue> PeralteCuadroFinal.play() </color>");
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
            Debug.Log("PeralteCuadroFinal: Start");
        }
    
    }
}
