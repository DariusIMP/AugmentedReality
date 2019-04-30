using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlanetInfoDisplayer : DisplayMenu
{
	public GameObject EncabezadoInformacion;
	
	public GameObject InfoSol,
		InfoMercurio,
		InfoVenus,
		InfoTierra,
		InfoMarte,
		InfoJupiter,
		InfoSaturno,
		InfoUrano,
		InfoNeptuno;
	
	private bool MenuDisplayed;

	private Text header;
	
	// Use this for initialization
	void Start ()
	{
		menuDisplay = InfoSol;
		header = EncabezadoInformacion.GetComponent<Text>();
		header.text = "Sol";
	}

	public enum Planet
	{
		Sun,
		Mercury,
		Venus,
		Earth,
		Mars,
		Jupiter,
		Saturn,
		Uranus,
		Neptune
	}

	public void HidePlanetInfo()
	{
		InfoSol.SetActive(false);
		InfoMercurio.SetActive(false);
		InfoVenus.SetActive(false);
		InfoTierra.SetActive(false);
		InfoMarte.SetActive(false);
		InfoJupiter.SetActive(false);
		InfoSaturno.SetActive(false);
		InfoUrano.SetActive(false);
		InfoNeptuno.SetActive(false);
	}
	
	public void Action() {
		if (!menuDisplay.activeInHierarchy)
		{
			menuDisplay.SetActive(true);
			MenuDisplayed = true;
		} else
		{
			menuDisplay.SetActive(false);
			MenuDisplayed = false;
		}
	}

	//facking harcodeo, no me importa nada
	public void DisplayPlanetInfo(int planet)
	{
		HidePlanetInfo();
		Planet _planet = (Planet) planet;
		
		switch (_planet)
		{
			case Planet.Sun :
				menuDisplay = InfoSol;
				header.text = "Sol";
				break;
			case Planet.Mercury :
				menuDisplay = InfoMercurio;
				header.text = "Mercurio";
				break;
			case Planet.Venus :
				menuDisplay = InfoVenus;
				header.text = "Venus";
				break;
			case Planet.Earth :
				menuDisplay = InfoTierra;
				header.text = "Tierra";
				break;
			case Planet.Mars :
				menuDisplay = InfoMarte;
				header.text = "Marte";
				break;
			case Planet.Jupiter :
				menuDisplay = InfoJupiter;
				header.text = "Júpiter";
				break;
			case Planet.Saturn :
				menuDisplay = InfoSaturno;
				header.text = "Saturno";
				break;
			case Planet.Uranus :
				menuDisplay = InfoUrano;
				header.text = "Urano";
				break;
			case Planet.Neptune :
				menuDisplay = InfoNeptuno;
				header.text = "Neptuno";
				break;
			}
		
		if(MenuDisplayed) menuDisplay.SetActive(true);
		}
}
