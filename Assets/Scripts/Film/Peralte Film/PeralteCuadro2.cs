using System.Collections;
using PeralteScripts;
using UnityEngine;

namespace Film.Peralte_Film
{
    public class PeralteCuadro2 : CuadroPeralte {

     /**_____________________________SCRIPT________________________________:
      * Para que un cuerpo se mueva en un MCU como es el caso de este
      * auto es necesario que no haya fuerzas verticales y que haya una
      * fuerza centrípeta (hacia el centro de la circunferencia y horizontal),
      * igual a mv²/R [Ponemos una flecha grande hacia el centro con mv²/R
      * como rótulo y aparece la velocidad]. Recordemos que esta fuerza no
      * cambia la magnitud de la velocidad, pero si su dirección, y por eso
      * la trayectoria es la de una circunferencia.”
      * ____________________________________________________________________
      */

        /// <summary>
        /// Activa el segundo audio (con subtitulo).
        /// La flecha central con mv^2/R aparece a los 10 segundos del audio.
        /// </summary>

		public override void Setup() {
			CambiarTexturaPistaARuta();
			PeralteManager pm = PeralteFilm.PeralteManager.GetComponent<PeralteManager> ();
			pm.switchView (PeralteManager.view.mcu);
		}

        public override void Play()
        {
            Debug.Log("<color=blue> PeralteCuadroSegundo.play() </color>");
            base.Play();
            DialogueManager.Play();
            
            Holograma.SetActive(false);
            Diagrama2D.SetActive(false);
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
			Debug.Log ("Cuadro 2:");
        }

        protected override void Start()
        {
            base.Start();
            Debug.Log("PeralteCuadroSegundo: Start");
        }

        public override void Stop()
        {
            Debug.Log("<color=blue> PeralteCuadroSegundo.stop() </color>");
            base.Stop();
        }    
        
    }
}
