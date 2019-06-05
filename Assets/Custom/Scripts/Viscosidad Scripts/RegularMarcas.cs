using UnityEngine;

namespace Viscosidad_Scripts
{
	public class RegularMarcas : MonoBehaviour
	{
		private static double upperMax = 2.583;
		private static double lowerMax = -2.568;
	
		public GameObject marca;

		public void Regular_Marca(string input)
		{
//		160 - 100
//		reg - reg / 160 * 100 -> porcentaje;

			float reg = float.Parse(input);
			double porcentaje = (reg / 160) * 100;
		
//		5 - 100
//		porcentaje - porcentaje / 5 * 100 -> posicion;

			double pos_y = (porcentaje / 100) * (upperMax - lowerMax);
//		posicion real = posicion - 2.5;
			pos_y = pos_y + lowerMax;

			Vector3 pos_local = marca.transform.localPosition;
			pos_local.y = (float)pos_y;
			marca.transform.localPosition = pos_local;
		}
	
		// Use this for initialization
		void Start () {
		}
	
		// Update is called once per frame
		void Update () {
		
		}
	}
}
