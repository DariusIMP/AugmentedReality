using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

namespace Film
{

	public abstract class Film : MonoBehaviour
	{
		public TogglePlayImage playButton;

		protected Cuadro CuadroActual;

		public List<Cuadro> secuencia;

		protected virtual void Start()
		{
			secuencia.Insert(0, gameObject.AddComponent<CuadroIdle>());
				
			CuadroActual = secuencia[0];
		}
	
		public bool IsCuadroInicial()
		{
			return secuencia.First().Equals(CuadroActual);
		}

		public bool IsCuadroFinal()
		{
			return secuencia.Last().Equals(CuadroActual);
		}

		public void PlayForward()
		{
			Debug.Log(name + ": PlayForward");
			try
			{
				CuadroActual.Stop();
				CuadroActual = secuencia[secuencia.IndexOf(CuadroActual) + 1];
				CuadroActual.Setup();
				CuadroActual.Play();
				playButton.setPauseButtonAvailible();
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine(e);
				CuadroActual = secuencia[0];
				throw;
			}
		}

		public void PlayBackwards()
		{
			Debug.Log(name + ": PlayBackwards"); 
			try
			{
				CuadroActual.Stop();
				CuadroActual = secuencia[secuencia.IndexOf(CuadroActual) - 1];
				CuadroActual.Setup();
				CuadroActual.Play();
				playButton.setPauseButtonAvailible();
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine(e);
				CuadroActual = secuencia[0];
				throw;
			}
		}

		public void ChooseCuadro(int index) {
			// Debería haber una mejor forma que pasar el índice.
			CuadroActual.Stop ();
			CuadroActual = secuencia[index];
			CuadroActual.Setup ();
		}

		public void Rewind()
		{
			playButton.setPlayButtonAvailible();
			CuadroActual.Stop();
			CuadroActual = secuencia[0];
		}

		public void FastForward()
		{
			playButton.setPlayButtonAvailible();
			CuadroActual.Stop();
			CuadroActual = secuencia.Last();
		}
		
		public void togglePlay() 
		{
			if (CuadroActual.Equals(secuencia[0]))
			{
				CuadroActual = secuencia[1];
			}
			playButton.setPauseButtonAvailible();
			CuadroActual.togglePlay ();
		}
	}
}
