using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PeralteScripts;

namespace Film.Peralte_Film
{
    public class PeralteCuadro6 : CuadroPeralte
    {

        /**_____________________________SCRIPT________________________________:
          * Ahora, hallemos la velocidad necesaria para tomar la curva sin roce.
            La tangente del angulo del peralte es igual a la Fuerza Centrípeta sobre el Peso.
            FcP=tg(0)
            Desarrollando la fuerza centrípeta en función de la velocidad y 
            el peso en función de la masa y la aceleración de la gravedad, nos queda:
            mv²Rmg=tg(0)
            Despejando la velocidad:
            |v| =sqrt(R·g·tg(0))
            Es interesante ver cómo la velocidad no depende de la masa, solo del ángulo de peralte y del radio.
            ____________________________________________________________________
          */

		public override void Setup() {
			PeralteManager pm = PeralteFilm.PeralteManager.GetComponent<PeralteManager> ();
			pm.switchView (PeralteManager.view.sinRozamiento);
			pm.switchFBD (Fbd.XY);
			CambiarTexturaPistaAHielo();
		}

        public override void Play()
        {
            Debug.Log("<color=blue> PeralteCuadroSexto.play() </color>");
            base.Play();

            ConfigureScene();

            //StartCoroutine(DesaparecerLasFormulasPrevias());
            StartCoroutine(AparicionDeLaFormula8());
            StartCoroutine(AparicionDeLaFormula9());
            StartCoroutine(AparicionDeLaFormula10());

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
            Debug.Log("PeralteCuadroSexto: Start");
        }

        public override void ConfigureScene()
        {
            Holograma.SetActive(true);
            Diagrama2D.SetActive(true);
            SectionTitle.text = "";


            Auto_Holograma.SetActive(true);
            Peso.SetActive(true);
            Normal.SetActive(true);
            
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
            
            NormalY.SetActive(true);
            NormalX.SetActive(true);
            PlanoCartesiano.SetActive(true);
            
        }

        private IEnumerator AparicionDeLaFormula8()
        {
            yield return new WaitForSecondsRealtime(9f);
            FormulaSRoz3.SetActive(true);
            StartCoroutine(FadingEffects.ShowImageFading(1f, FormulaSRoz3.GetComponent<Image>()));
        }

        private IEnumerator AparicionDeLaFormula9()
        {
            yield return new WaitForSecondsRealtime(23f);
            FormulaSRoz4.SetActive(true);
            StartCoroutine(FadingEffects.ShowImageFading(1f, FormulaSRoz4.GetComponent<Image>()));
        }

        private IEnumerator AparicionDeLaFormula10()
        {
            yield return new WaitForSecondsRealtime(27f);
            FormulaSRoz5.SetActive(true);
            StartCoroutine(FadingEffects.ShowImageFading(1f, FormulaSRoz5.GetComponent<Image>()));
        }
        
        private IEnumerator DesaparecerLasFormulasPrevias()
        {
            yield return new WaitForSecondsRealtime(1f);
            StartCoroutine(FadingEffects.HideImageFading(1f, FormulaSRoz1.GetComponent<Image>()));
            StartCoroutine(FadingEffects.HideImageFading(1f, FormulaSRoz2.GetComponent<Image>()));
        }
    }
}
