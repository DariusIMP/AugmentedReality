using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

/**
 * PlayBarManager
 * Maneja los botones del PlayBar que controlan la reproducción
 * del Film.
 * Esta clase se comunica con la clase Film para indicarle que
 * avance o retroceda de cuadro. A su vez la clase Film se comunica
 * con el PlayBarManager ya que le debe decir cuando la película
 * está en el inicio o al final, para que el PlayBarManager
 * deshabilite los botones de retroceder / avanzar la película.
 */
public class PlayBarManager : MonoBehaviour
{
	private Film.Film _film;
	public Button PlayButton, PrevButton, NextButton, FirstButton, LastButton;
	public GameObject Augmento;

	private void Start()
	{
		_film = Augmento.GetComponentInChildren<Film.Film>();
	}

	private void Update()
	{
		if (_film.IsCuadroInicial())
		{
			DisableBackwards();
		}
		else
		{
			EnableBackwards();
		}

		if (_film.IsCuadroFinal())
		{
			DisableFrontwards();
		}
		else
		{
			EnableFrontwards();
		}
	}

	//DisableBackwards
	//Desactiva los botones de ir a la escena anterior y a la primera escena.
	//Para ello desactiva la interactuabilidad de los botones y modifica el
	//color de los mismos a un color más oscuro.
	//Esto es para cuando el Film esté justo al inicio.
	public void DisableBackwards()
	{
		PrevButton.interactable = false;
		FirstButton.interactable = false;
	}

	//EnableBackwards
	//Contrario a DisableBackwards, activa los botones de ir a la escena anterior
	//y a la primera escena. Esto es para cuando el Film no esté en el inicio.
	public void EnableBackwards()
	{
		PrevButton.interactable = true;
		FirstButton.interactable = true;
	}

	//TODO : ¿desactivar el botón de play?
	//DisableFrontwards
	//Desactiva los botones de ir a la escena posterior y a la última escena.
	//Para ello desactiva la interactuabilidad de los botones y modifica el
	//color de los mismos a un color más oscuro.
	//Esto es para cuando el Film esté justo al final.
	public void DisableFrontwards()
	{
		NextButton.interactable = false;
		LastButton.interactable = false;
	}
	
	//EnableFrontwards
	//Contrario a DisableFrontwards, activa los botones de ir a la escena anterior
	//y a la primera escena. Esto es para cuando el Film no esté en el inicio.
	public void EnableFrontwards()
	{
		NextButton.interactable = true;
		LastButton.interactable = true;
	}

}
