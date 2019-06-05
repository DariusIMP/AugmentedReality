using UnityEngine;

namespace Film
{
	/**
     * CuadroIdle
     * Cuadro inicial que no hace nada que se ubica justo al principio del film
     * (de hecho Film lo inserta automáticamente). Esto es para que el usuario
     * le de a reproducir para que recién se active el primer cuadro.
     */
	public class CuadroIdle : Cuadro
	{
		public override void Setup() {

		}

		public override void Play(){}

		public override void Stop(){}

		private new void Start()
		{
			Debug.Log(name + ": Idle");
		}
	}
}