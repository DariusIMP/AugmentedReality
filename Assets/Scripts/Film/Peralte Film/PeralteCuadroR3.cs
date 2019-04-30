using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PeralteScripts;

namespace Film.Peralte_Film
{
    public class PeralteCuadroR3 : CuadroPeralte
    {

        /**_____________________________SCRIPT________________________________:
          * -Velocidad mínima-
           ____________________________________________________________________
          */

		public override void Setup() {
			CambiarTexturaPistaARuta();
			PeralteManager pm = PeralteFilm.PeralteManager.GetComponent<PeralteManager> ();
			pm.switchView (PeralteManager.view.conRozamientoVMin);
			pm.switchFBD (Fbd.FuerzasReales);
		}

        public override void Play()
        {
            Debug.Log("<color=blue> PeralteCuadroSexto.play() </color>");
            base.Play();

            Holograma.SetActive(true);
            Diagrama2D.SetActive(true);
            SectionTitle.text = "";

            Auto_Holograma.SetActive(true);
            PlanoCartesiano.SetActive(true);

            Peso.SetActive(true);
            Normal.SetActive(true);
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
            RozVmin.SetActive(true);
            RozVminX.SetActive(false);
            RozVmaxY.SetActive(false);
            RozVmaxX.SetActive(false);
            RozVmax.SetActive(false);
            
            StartCoroutine(AparicionDelTituloVelocidadMinima());

            StartCoroutine(AparecerImagen(18f, RozVminX));
            StartCoroutine(AparecerImagen(18f, RozVminY));
            StartCoroutine(AparecerImagen(18f, NormalX));
            StartCoroutine(AparecerImagen(18f, NormalY));
            
            StartCoroutine(AparecerImagen(28f, FormulaVMin0));
            StartCoroutine(AparecerImagen(34f, FormulaVMin1));
            StartCoroutine(AparecerImagen(43f, FormulaVMin2));
            StartCoroutine(AparecerImagen(50f, FormulaVMin3));
            StartCoroutine(AparecerImagen(58f, FormulaVMin4));
            StartCoroutine(AparecerImagen(63f, FormulaVMin5));
            
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

 
        private IEnumerator AparicionDelTituloVelocidadMinima()
        {
            Text titulo = SectionTitle.GetComponent<Text>();
            titulo.text = "Velocidad mínima";
            yield return StartCoroutine(FadingEffects.ShowAndHideTextFading(0.5f, 1f, titulo));
        }

        private IEnumerator AparecerImagen(float momento, GameObject elemento)
        {
            yield return new WaitForSecondsRealtime(momento);
            elemento.SetActive(true);
            yield return StartCoroutine(FadingEffects.ShowImageFading(1f, elemento.GetComponent<Image>()));
        }
    }
}
