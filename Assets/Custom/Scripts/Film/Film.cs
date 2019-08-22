using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Film
{

	public abstract class Film : MonoBehaviour
	{
		public TogglePlayImage playButton;
        public Button PrevButton;
        public Button NextButton;
        public Button LastButton;
        public Button FirstButton;

		protected Cuadro CuadroActual;
        protected int IdxActual;

		public List<Cuadro> secuencia;

		protected virtual void Start()
		{
            IdxActual = 0;
            CuadroActual = secuencia[IdxActual];
            UpdateButtons();
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
                PrevButton.interactable = true;
                FirstButton.interactable = true;
                ChooseCuadro(++IdxActual);
                CuadroActual.Play();
				playButton.setPauseButtonAvailible();
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine(e);
                IdxActual = secuencia.Count - 1;
				CuadroActual = secuencia[IdxActual];
				throw;
			}
            finally
            {
                UpdateButtons();
            }
		}

		public void PlayBackwards()
		{
			Debug.Log(name + ": PlayBackwards"); 
			try
			{
                NextButton.interactable = true;
                LastButton.interactable = true;
                ChooseCuadro(--IdxActual);
				CuadroActual.Play();
				playButton.setPauseButtonAvailible();
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine(e);
                IdxActual = 0;
				CuadroActual = secuencia[IdxActual];
				throw;
			}
            finally
            {
                UpdateButtons();
            }
		}

		public void ChooseCuadro(int index) {
			// Debería haber una mejor forma que pasar el índice.
			CuadroActual.Stop ();
            IdxActual = index;
            CuadroActual = secuencia[index];
            CuadroActual.Setup();
        }

		public void Rewind()
		{
			playButton.setPlayButtonAvailible();
			CuadroActual.Stop();
            IdxActual = 0;
            CuadroActual = secuencia[IdxActual];
			CuadroActual.ConfigureScene();
            UpdateButtons();
		}

		public void FastForward()
		{
			playButton.setPlayButtonAvailible();
			CuadroActual.Stop();
            IdxActual = secuencia.Count - 1;
			CuadroActual = secuencia[IdxActual];
			CuadroActual.ConfigureScene();
            UpdateButtons();
		}
		
		public void togglePlay() 
		{
			if (IdxActual == 0)
			{
                IdxActual = 1;
				CuadroActual = secuencia[IdxActual];
                UpdateButtons();
			}
			playButton.setPauseButtonAvailible();
			CuadroActual.togglePlay ();
		}

        private void UpdateButtons()
        {
            PrevButton.interactable = FirstButton.interactable = !IsCuadroInicial();
            NextButton.interactable = LastButton.interactable = !IsCuadroFinal();
        }
	}
}
