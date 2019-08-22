using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Film.Peralte_Film
{
    public class PeralteCuadroR1 : CuadroPeralte
    {

        /**_____________________________SCRIPT________________________________:
          *  -Intro Con rozamiento-
           ____________________________________________________________________
          */


		public override void Setup() {
			CambiarTexturaPistaARuta();
			StartCoroutine(AparicionDelTituloConRozamiento());
		}

        public override void Play()
        {
            Debug.Log("<color=blue> PeralteCuadroSexto.play() </color>");
            base.Play();
            ConfigureScene();
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
            PlanoCartesiano.SetActive(true);

            Peso.SetActive(false);
            Normal.SetActive(false);
            NormalY.SetActive(false);
            NormalX.SetActive(false);
                   
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
        }

        private IEnumerator AparicionDelTituloConRozamiento()
        {
            Text titulo = SectionTitle.GetComponent<Text>();
            titulo.text = "Con rozamiento";
            yield return StartCoroutine(FadingEffects.ShowAndHideTextFading(0.5f, 1f, titulo));
        }

        private IEnumerator AparicionFormulaVMax(float momento, GameObject formula)
        {
            yield return new WaitForSecondsRealtime(momento);
            formula.SetActive(true);
            yield return StartCoroutine(FadingEffects.ShowImageFading(1f, formula.GetComponent<Image>()));
        }
        
    }
}
