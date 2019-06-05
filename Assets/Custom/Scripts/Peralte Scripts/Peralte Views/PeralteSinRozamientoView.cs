using System;
using UnityEngine;

public class PeralteSinRozamientoView : FBDView
{
	
	public override void Start() {
		peralteProblem = new PeralteSinRozamiento (masa, velocidad, mu: 0);
		init ();
	}

	public override void construirFBDFuerzasReales() {
		FBDFuerzasReales = instantiateFBD("FBDFuerzasReales");

		FreeBodyDiagram FBD = FBDFuerzasReales.GetComponent<FreeBodyDiagram>();
		FBD.addArrow(0f, 90f, 0f, velocidad, "V");
		// Peso.
		FBD.addArrow(270f + angulo, peralteProblem.P, "P");
		Debug.Log ("angulo del sin rozamiento: " + angulo);
		// Normal.
		FBD.addArrow(90f, peralteProblem.Normal, "N");
		// TODO agregar rozamiento.
	}

	public override void construirFBDXY() {
		FBDXY = instantiateFBD ("FBDXY");

		FreeBodyDiagram FBD = FBDXY.GetComponent<FreeBodyDiagram>();
		FBD.addArrow(0f, 90f, 0f, velocidad, "V");
		// Peso.
		FBD.addArrow(270f + angulo, peralteProblem.P, "P");
		// Fuerza Centripeta.
		FBD.addArrow(0f + angulo, peralteProblem.fuerzaCentripeta, "Nx = Fc");
		// Normal en Y, que compenza con el peso.
		FBD.addArrow(90f + angulo, peralteProblem.Ny, "Ny");
	}
}