using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PeralteScripts;

namespace Film.Peralte_Film
{
    public class PeralteCuadro5 : CuadroPeralte {

     /**_____________________________SCRIPT________________________________:
      * Horizontalmente, la componente X de la normal será la fuerza centrípeta.
      * Esto tiene mucho sentido: la normal es la fuerza que ejerce la pista
      * sobre el auto. Está fuerza es la que va a hacer que el auto mantenga
      * la trayectoria circular.
      *     x: Fc = Nx = Nsin()
      * ____________________________________________________________________
      */

		public override void Setup() {
			PeralteManager pm = PeralteFilm.PeralteManager.GetComponent<PeralteManager> ();
			pm.switchView (PeralteManager.view.sinRozamiento);
			pm.switchFBD (Fbd.XY);
			CambiarTexturaPistaAHielo();
		}

        public override void Play()
        {
            Debug.Log("<color=blue> PeralteCuadroQuinto.play() </color>");
            base.Play();
            
            Holograma.SetActive(true);
            Diagrama2D.SetActive(true);
            SectionTitle.text = "";


            Auto_Holograma.SetActive(true);
            Peso.SetActive(true);
            Normal.SetActive(true);
            
            FormulaSRoz1.SetActive(true);
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
            
            NormalY.SetActive(true);
            NormalX.SetActive(true);
            PlanoCartesiano.SetActive(true);

            StartCoroutine(AparicionDeLaFormula7());
            
            DialogueManager.Play();
        }

        protected override void Start()
        {
            base.Start();
            Debug.Log("PeralteCuadroQuinto: Start");
        }

        public override void Stop()
        {
            Debug.Log("<color=blue> PeralteCuadroSegundo.stop() </color>");
            base.Stop();
        }    
        
        private IEnumerator AparicionDeLaFormula7()
        {
            yield return new WaitForSecondsRealtime(4f);        
            FormulaSRoz2.SetActive(true);
            StartCoroutine(FadingEffects.ShowImageFading(1f, FormulaSRoz2.GetComponent<Image>()));
        }

        
    }
}
