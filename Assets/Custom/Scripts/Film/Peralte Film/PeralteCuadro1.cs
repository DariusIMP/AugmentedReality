using UnityEngine;
using UnityEngine.UI;

namespace Film.Peralte_Film
{
	public class PeralteCuadro1 : CuadroPeralte{

		/**
		 * _____________________________SCRIPT________________________________:
		[El auto comienza girando para mostrar “lo que debe ocurrir”. Sin DLC.] 
		El peralte es la inclinación lateral de un camino en curva, donde el 
		borde exterior es más alto que el interior. La rapidez del auto y la 
		magnitud de su velocidad son constantes pero en cada instante cambia
		de dirección debido al peralte que impide que el vehículo se salga de
		la pista. Así el vehículo tiene aceleración normal (o centrípeta) pero
		no tangencial.
		_______________________________________________________________________
		*/

		/// <summary>
		/// No pasa nada, se reproduce el audio y los subtítulos introductorios
		/// del problema del peralte nada más.
		/// </summary>

		public override void Setup() {

		}

		public override void Play()
		{
			Debug.Log("<color=blue> PeralteCuadroInicial.play() </color>");
			base.Play();
			DialogueManager.Play();

			ConfigureScene();
		}

		public override void Stop()
		{
			Debug.Log("<color=blue> PeralteCuadroSegundo.stop() </color>");
			base.Stop();
		}    
		
		protected override void Start()
		{
			base.Start();
			Debug.Log("PeralteCuadroInicial: Start");

		}

		public override void ConfigureScene()
		{
			Holograma.SetActive(false);
			Diagrama2D.SetActive(false);
			
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
			Auto_Holograma.SetActive(false);
			PlanoCartesiano.SetActive(false);

			SectionTitle.text = "";
			
		}
	}
}
