using UnityEngine;

namespace Viscosidad_Scripts
{
	/**
	 * Controlador del diagrama de cuerpo libre centrado en la esfera
	 * en el experimento de medición de la viscosidad de un fluído.
	 */
	public class DclController : MonoBehaviour
	{
		public GameObject Peso;
		public GameObject Viscosidad;
		public GameObject Normal;

		public void SetScale(GameObject vector, float scale)
		{
			var newScale = new Vector3
			{
				x = vector.transform.localScale.x,
				y = vector.transform.localScale.y,
				z = scale
			};
			vector.transform.localScale = newScale;
		}
	}
}
