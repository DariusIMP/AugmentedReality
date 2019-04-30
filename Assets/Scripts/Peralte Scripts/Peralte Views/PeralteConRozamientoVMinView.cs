using System;

public class PeralteConRozamientoVMinView : FBDView
{
	public override void Start() {
		peralteProblem = new PeralteConRozamientoVMin (masa, velocidad, mu);
		init ();
	}

	public override void construirFBDFuerzasReales() {
		FBDFuerzasReales = instantiateFBD("FBDFuerzasReales");

		FreeBodyDiagram FBD = FBDFuerzasReales.GetComponent<FreeBodyDiagram>();
		FBD.addArrow(0f, 90f, 0f, velocidad, "V");

		// Peso.
		FBD.addArrow(270f + angulo, peralteProblem.P, "P");

		// Normal.
		FBD.addArrow(90f, peralteProblem.Normal, "N");

		// Rozamiento hacia afuera.
		FBD.addArrow(180f, peralteProblem.Fr, "Fr");
	}

	public override void construirFBDXY() {
		FBDXY = instantiateFBD ("FBDXY");

		FreeBodyDiagram FBD = FBDXY.GetComponent<FreeBodyDiagram>();
		FBD.addArrow(0f, 90f, 0f, velocidad, "V");

		// Peso.
		FBD.addArrow(270f + angulo, peralteProblem.P, "P");

		// Rozamiento en X y en Y.
		FBD.addArrow(180f + angulo, peralteProblem.Frx, "Frx");
		FBD.addArrow(90f + angulo, peralteProblem.Fry, "Fry");

		// Normal en x y en Y.
		FBD.addArrow(angulo, peralteProblem.Nx, "Nx");
		FBD.addArrow(90f + angulo, peralteProblem.Ny, "Ny");
	}
}


