using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PeralteScripts;

namespace Film.Peralte_Film
{
    public class PeralteCuadroR2 : CuadroPeralte
    {

        /**_____________________________SCRIPT________________________________:
          * -Velocidad Máxima-
           ____________________________________________________________________
          */

		public override void Setup() {
			CambiarTexturaPistaARuta();
			PeralteManager pm = PeralteFilm.PeralteManager.GetComponent<PeralteManager> ();
			pm.switchView (PeralteManager.view.conRozamientoVMax);
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
            RozVmin.SetActive(false);
            RozVminX.SetActive(false);
            RozVmaxY.SetActive(false);
            RozVmaxX.SetActive(false);
            RozVmax.SetActive(true);
            
            StartCoroutine(AparicionDelTituloVelocidadMaxima());

            StartCoroutine(AparecerImagen(8f, RozVmaxX));
            StartCoroutine(AparecerImagen(8f, RozVmaxY));
            StartCoroutine(AparecerImagen(8f, NormalX));
            StartCoroutine(AparecerImagen(8f, NormalY));
            
            StartCoroutine(AparecerImagen(19f, FormulaVMax0));
            StartCoroutine(AparecerImagen(27f, FormulaVMax1));
            StartCoroutine(AparecerImagen(35f, FormulaVMax2));
            StartCoroutine(AparecerImagen(53f, FormulaVMax3));
            StartCoroutine(AparecerImagen(65f, FormulaVMax4));
            StartCoroutine(AparecerImagen(70f, FormulaVMax5));
            
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

 
        private IEnumerator AparicionDelTituloVelocidadMaxima()
        {
            Text titulo = SectionTitle.GetComponent<Text>();
            titulo.text = "Velocidad máxima";
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
