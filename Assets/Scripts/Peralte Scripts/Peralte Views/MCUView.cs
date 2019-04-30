using System;
using UnityEngine;

public class MCUView : PeralteView
{
	protected GameObject FBDMCU;
	public float velocidad = 30;

	public override void Start() {
		FBDMCU = instantiateFBD ("MCU");
		FreeBodyDiagram FBDScript = FBDMCU.GetComponent<FreeBodyDiagram> ();
		FBDScript.addArrow (20f, 150, "Fc");
		FBDScript.addArrow (0, 90, 0, velocidad, "V");
	}

	public MCUView ()
	{
	}

	public override void setActive(bool active) {
		if (FBDMCU) {
			FBDMCU.SetActive (active);
		}
	} 
}