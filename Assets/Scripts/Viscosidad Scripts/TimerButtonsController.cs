using UnityEngine;

namespace Viscosidad_Scripts
{
	public class TimerButtonsController : MonoBehaviour
	{

		public GameObject BotonDisponer, BotonSoltar, BotonCronometrar, BotonDetener;

		void Start () {
			BotonSoltar.SetActive(true);
			BotonDisponer.SetActive(false);
			BotonCronometrar.SetActive(true);
			BotonDetener.SetActive(false);
		}

		public void onDisponerClicked()
		{
			BotonSoltar.SetActive(true);
			BotonDisponer.SetActive(false);
		}

		public void onSoltarClicked()
		{
			BotonSoltar.SetActive(false);
			BotonDisponer.SetActive(true);
		}

		public void onCronometrarClicked()
		{
			BotonCronometrar.SetActive(false);
			BotonDetener.SetActive(true);
		}

		public void onDetenerClicked()
		{
			BotonCronometrar.SetActive(true);
			BotonDetener.SetActive(false);
		}
	}
}
