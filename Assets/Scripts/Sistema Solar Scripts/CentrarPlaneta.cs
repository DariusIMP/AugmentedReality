using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentrarPlaneta : MonoBehaviour
{
	
	public GameObject SolarSystem;
	public GameObject Sun, Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune;
	public Animator _animator;
	

	private float mult = 30;
	private Vector3 sunScale;
	private Vector3 mercuryScale;
	private Vector3 venusScale;
	private Vector3 earthScale;
	private Vector3 marsScale;
	private Vector3 jupiterScale;
	private Vector3 saturnScale;
	private Vector3 uranusScale;
	private Vector3 neptuneScale;

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

	public Planet _central = Planet.Sun;

	public void CentrarSobre(int planeta)
	{
		_central = (Planet)planeta;
	}

	private void Start()
	{
		sunScale 		= new Vector3(0.007f, 0.007f, 0.007f);
		mercuryScale	= new Vector3(0.2f, 0.2f, 0.2f);
		venusScale 		= new Vector3(0.06f, 0.06f, 0.06f);
		earthScale	 	= new Vector3(0.12f, 0.12f, 0.12f);
		marsScale 		= new Vector3(0.2f, 0.2f, 0.2f);
		jupiterScale 	= new Vector3(0.02f, 0.02f, 0.02f);
		saturnScale 	= new Vector3(0.03f, 0.03f, 0.03f);
		uranusScale 	= new Vector3(0.025f, 0.025f, 0.025f);
		neptuneScale 	= new Vector3(0.04f, 0.04f, 0.04f);
		
		//Harcodiiing!!!
		sunScale 	*= mult;
		mercuryScale*= mult;
		venusScale 	*= mult;
		earthScale	 *= mult;
		marsScale 	*= mult;
		jupiterScale *= mult;
		saturnScale *= mult;
		uranusScale *= mult;
		neptuneScale *= mult;

	}

	void Update ()
	{
		switch (_central)
		{
			case Planet.Sun :
				SolarSystem.transform.localScale = sunScale;
				Ajustar_Velocidad(1f);
				SolarSystem.transform.Translate(-Sun.transform.position);
				break;
			case Planet.Mercury : 
				SolarSystem.transform.localScale = mercuryScale;
				Ajustar_Velocidad(0.05f);
				SolarSystem.transform.Translate(-Mercury.transform.position);
				break;
			case Planet.Venus : 
				SolarSystem.transform.localScale = venusScale;
				Ajustar_Velocidad(0.6f);
				SolarSystem.transform.Translate(-Venus.transform.position);
				break;
			case Planet.Earth : 
				SolarSystem.transform.localScale = earthScale;
				Ajustar_Velocidad(0.1f);
				SolarSystem.transform.Translate(-Earth.transform.position);
				break;
			case Planet.Mars : 
				SolarSystem.transform.localScale = marsScale;
				Ajustar_Velocidad(0.1f);
				SolarSystem.transform.Translate(-Mars.transform.position);
				break;
			case Planet.Jupiter : 
				SolarSystem.transform.localScale = jupiterScale;
				Ajustar_Velocidad(0.3f);
				SolarSystem.transform.Translate(-Jupiter.transform.position);
				break;
			case Planet.Saturn : 
				SolarSystem.transform.localScale = saturnScale;
				Ajustar_Velocidad(0.3f);
				SolarSystem.transform.Translate(-Saturn.transform.position);
				break;
			case Planet.Uranus : 
				SolarSystem.transform.localScale = uranusScale;
				Ajustar_Velocidad(0.5f);
				SolarSystem.transform.Translate(-Uranus.transform.position);
				break;
			case Planet.Neptune : 
				SolarSystem.transform.localScale = neptuneScale;
				Ajustar_Velocidad(0.5f);
				SolarSystem.transform.Translate(-Neptune.transform.position);
				break;
		}
	}
	
	public void Ajustar_Velocidad(float velocidad)
	{
		_animator.speed = velocidad;
	}	  
}
