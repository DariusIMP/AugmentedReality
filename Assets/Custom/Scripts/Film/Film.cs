using System;
using System.Collections.Generic;
using Film;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.Film
{

	public abstract class Film : MonoBehaviour
	{
		public TogglePlayImage playButton;
        public Button PrevButton;
        public Button NextButton;
        public Button LastButton;
        public Button FirstButton;
        public bool AudioOn;
		protected Cuadro CuadroActual;
        protected int IdxActual;

		public List<Cuadro> secuencia;

		protected virtual void Start()
		{
            IdxActual = 0;
            CuadroActual = secuencia[IdxActual];
            UpdateInteractability();
            if (!AudioOn)
            {
	            CuadroActual.TurnAudioOff();
            }
		}
	
		public bool IsCuadroInicial()
		{
			return IdxActual == 0;
		}

		public bool IsCuadroFinal()
		{
			return IdxActual == secuencia.Count - 1;
		}

		public void PlayForward()
		{
			Debug.Log(name + ": PlayForward");
			try
			{
                ChooseCuadro(++IdxActual);
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine(e);
                IdxActual = secuencia.Count - 1;
				CuadroActual = secuencia[IdxActual];
                UpdateInteractability();
                throw;
			}
		}

		public void PlayBackwards()
		{
			Debug.Log(name + ": PlayBackwards"); 
			try
			{
                ChooseCuadro(--IdxActual);
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine(e);
                IdxActual = 0;
				CuadroActual = secuencia[IdxActual];
                UpdateInteractability();
                throw;
			}
		}

		public void ChooseCuadro(int index) {
			// Debería haber una mejor forma que pasar el índice.
			CuadroActual.Stop ();
            IdxActual = index;
            CuadroActual = secuencia[index];
            CuadroActual.Setup();
            CuadroActual.Play();
            playButton.setPauseButtonAvailible();
            UpdateInteractability();
        }

		public void Rewind()
		{
			playButton.setPlayButtonAvailible();
			CuadroActual.Stop();
            IdxActual = 0;
            CuadroActual = secuencia[IdxActual];
			CuadroActual.ConfigureScene();
            UpdateInteractability();
		}

		public void FastForward()
		{
			playButton.setPlayButtonAvailible();
			CuadroActual.Stop();
            IdxActual = secuencia.Count - 1;
			CuadroActual = secuencia[IdxActual];
			CuadroActual.ConfigureScene();
            UpdateInteractability();
		}
		
		public void togglePlay() 
		{
			if (false && IdxActual == 0)
			{
                IdxActual = 1;
				CuadroActual = secuencia[IdxActual];
                UpdateInteractability();
			}
			CuadroActual.togglePlay ();
		}

        private void UpdateInteractability()
        {
            PrevButton.interactable = FirstButton.interactable = !IsCuadroInicial();
            NextButton.interactable = LastButton.interactable = !IsCuadroFinal();
        }

        public Cuadro GetCuadroActual()
        {
	        return CuadroActual;
        }
	}
}
