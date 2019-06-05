using System.Collections;
using PeralteScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Film.Peralte_Film
{
    public class PeralteCuadro3 : CuadroPeralte {

     /**_____________________________SCRIPT________________________________:
      * [Pantalla que titule “Peralte sin rozamiento”]
      * [Cambia la textura de la pista a hielo]
      * “¿Cuáles son las fuerzas que interactúan en este problema? 
      * [Aparece el holograma con el diagrama de fuerzas reales]
      * Si el problema es sin rozamiento, las únicas fuerzas que habrá serán
      * la Normal [Aparece la normal] y el Peso [Aparece el peso].
      * Cómo no hay ninguna otra fuerza, la resultante de estas dos fuerzas
      * será la Fuerza Centrípeta y en particular es una de las componentes de
      * la Normal la que proporciona la aceleración centrípeta necesaria para
      * tomar la curva (esta es la fuerza centrípeta).”
      * ____________________________________________________________________
      */

		public override void Setup() {
			PeralteManager pm = PeralteFilm.PeralteManager.GetComponent<PeralteManager> ();
			pm.switchView (PeralteManager.view.sinRozamiento);
			pm.switchFBD (Fbd.FuerzasReales);
			CambiarTexturaPistaAHielo();
		}

        public override void Play()
        {
            Debug.Log("<color=blue> PeralteCuadroTercero.play() </color>");
            base.Play();

            Holograma.SetActive(true);
            Diagrama2D.SetActive(true);
            SectionTitle.text = "";
            
            Auto_Holograma.SetActive(false);
            Peso.SetActive(false);
            Normal.SetActive(false);
            
            FormulaSRoz1.SetActive(false);
            FormulaSRoz2.SetActive(false);
            FormulaSRoz3.SetActive(false);
            FormulaSRoz4.SetActive(false);
            FormulaSRoz5.SetActive(false);
            
            FormulaVMax0.SetActive(false);
            FormulaVMax1.SetActive(false);
            FormulaVMax2.SetActive(false);
            FormulaVMax3.SetActive(false);
            FormulaVMax4.SetActive(false);
            FormulaVMax5.SetActive(false);
			
            FormulaVMin0.SetActive(false);
            FormulaVMin1.SetActive(false);
            FormulaVMin2.SetActive(false);
            FormulaVMin3.SetActive(false);
            FormulaVMin4.SetActive(false);
            FormulaVMin5.SetActive(false);
            
            RozVminY.SetActive(false);
            RozVmin.SetActive(false);
            RozVminX.SetActive(false);
            RozVmaxY.SetActive(false);
            RozVmaxX.SetActive(false);
            RozVmax.SetActive(false);
            
            NormalY.SetActive(false);
            NormalX.SetActive(false);
            PlanoCartesiano.SetActive(false);
            
            StartCoroutine(AparicionDelTituloSinRozamiento());
            StartCoroutine(AparicionDelHolograma());
            StartCoroutine(AparicionDeLaNormal());
            StartCoroutine(AparicionDelPeso());
            
			Debug.Log ("Corroutine cuadro 2");
            DialogueManager.Play();
        }

        public override void Stop()
        {
            Debug.Log("<color=blue> PeralteCuadroSegundo.stop() </color>");
            base.Stop();
        }    
        
        private IEnumerator AparicionDelTituloSinRozamiento()
        {
            Text titulo = SectionTitle.GetComponent<Text>();
            titulo.text = "Sin rozamiento";
            yield return StartCoroutine(FadingEffects.ShowAndHideTextFading(0.5f, 1f, titulo));
        }

        private IEnumerator AparicionDelHolograma()
        {
            yield return new WaitForSecondsRealtime(20f);
            Auto_Holograma.SetActive(true);
            PlanoCartesiano.SetActive(true);
        }

        private IEnumerator AparicionDeLaNormal()
        {
            yield return new WaitForSecondsRealtime(24f);
            Normal.SetActive(true);
            StartCoroutine(FadingEffects.ShowImageFading(1f, Normal.GetComponent<Image>()));
        }

        private IEnumerator AparicionDelPeso()
        {
            yield return new WaitForSecondsRealtime(25f);
            Peso.SetActive(true);
            StartCoroutine(FadingEffects.ShowImageFading(1f, Peso.GetComponent<Image>()));
        }
        
        protected override void Start()
        {
            base.Start();
            Debug.Log("PeralteCuadroTercero: Start");
        }
    }
}
