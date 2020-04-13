using System.Collections;
using UnityEngine;
using PeralteScripts;
using UnityEngine.UI;

namespace Film.Peralte_Film
{
    public class PeralteCuadro4 : CuadroPeralte {

    /*_____________________________SCRIPT________________________________:
     * [Descomposición xy; aparecen los vectores pertinentes coloreados
     * distintamente a los vectores de las fuerzas reales. [¿Se hace desaparecer
     * el vector normal?]: “Como decíamos hace un rato, para que haya el MCU
     * tenemos dos condiciones, que la fuerza vertical sea nula, y que la
     * horizontal vaya hacia el centro de la circunferencia y sea mv²/R.
     * La sumatoria de las fuerzas en la dirección vertical es cero:
     * el peso y la componente Y de la normal se cancelan. “
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
            Debug.Log("<color=blue> PeralteCuadroCuarto.play() </color>");
            base.Play();
            ConfigureScene();

            StartCoroutine(AparicionDeLasDescomposicionesDeLaNormalEnX());
            StartCoroutine(AparicionDeLasDescomposicionesDeLaNormalEnY());
            StartCoroutine(AparicionDeLaFormulaDePesoIgualANormal());
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
            Debug.Log("PeralteCuadroCuarto: Start");
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
            
            NormalY.SetActive(false);
            NormalX.SetActive(false);
            PlanoCartesiano.SetActive(true);
            
        }

        private IEnumerator AparicionDeLasDescomposicionesDeLaNormalEnX()
        {
            yield return new WaitForSecondsRealtime(13f);        
            NormalX.SetActive(true);
            StartCoroutine(FadingEffects.ShowImageFading(1f, NormalX.GetComponent<Image>()));
        }

        private IEnumerator AparicionDeLasDescomposicionesDeLaNormalEnY()
        {
            yield return new WaitForSecondsRealtime(14f);
            NormalY.SetActive(true);       
            StartCoroutine(FadingEffects.ShowImageFading(1f, NormalY.GetComponent<Image>()));
        }

        private IEnumerator AparicionDeLaFormulaDePesoIgualANormal()
        {
            yield return new WaitForSecondsRealtime(17f);
            FormulaSRoz1.SetActive(true);
            StartCoroutine(FadingEffects.ShowImageFading(1f, FormulaSRoz1.GetComponent<Image>()));
        }
    }
}
