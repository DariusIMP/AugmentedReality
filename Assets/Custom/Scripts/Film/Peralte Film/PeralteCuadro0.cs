using Film.Peralte_Film;
using UnityEngine;

namespace Custom.Scripts.Film.Peralte_Film
{
	public class PeralteCuadro0 : CuadroPeralte{
		
		/// <summary>
		/// Escena inicial en la que se ve al auto girar sobre la pista sin	hacer nada.
		/// </summary>

		public override void Setup() {

		}

		public override void Play()
		{
			Debug.Log("<color=blue> PeralteCuadro0.play() </color>");
			base.Play();

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

			CambiarTexturaPistaARuta();

		}

	}
}
