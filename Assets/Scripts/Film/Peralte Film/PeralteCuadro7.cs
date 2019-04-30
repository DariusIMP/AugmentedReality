using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PeralteScripts;

namespace Film.Peralte_Film
{
    public class PeralteCuadro7 : CuadroPeralte
    {

        /**_____________________________SCRIPT________________________________:
          * 
           ____________________________________________________________________
          */

		public override void Setup() {
			PeralteManager pm = PeralteFilm.PeralteManager.GetComponent<PeralteManager> ();
			pm.hideFBD ();
		}

        public override void Play()
        {
            Debug.Log("<color=blue> PeralteCuadro7.play() </color>");
            base.Play();

            Holograma.SetActive(false);
            Diagrama2D.SetActive(false);
            SectionTitle.text = "";

            Auto_Holograma.SetActive(false);
            PlanoCartesiano.SetActive(false);

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
            
            DialogueManager.Play();
        }

        public override void Stop()
        {
            Debug.Log("<color=blue> PeralteCuadro7.stop() </color>");
            base.Stop();
        }    
        
        protected override void Start()
        {
            base.Start();
            Debug.Log("PeralteCuadro7: Start");
        }

   
    }
}
