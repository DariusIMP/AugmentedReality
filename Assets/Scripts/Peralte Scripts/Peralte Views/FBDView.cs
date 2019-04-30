using System;
using UnityEngine;

public enum Fbd
{
	NormalTangencial,
	XY,
	FuerzasReales
}

public abstract class FBDView : PeralteView
{
	// Diagramas de cuerpo libre que tendremos.
	protected GameObject FBDFuerzasReales; // Normal, peso, rozamiento.
	protected GameObject FBDXY; // PY, NY, NX,

	protected GameObject FBDActual;

	// Datos del problema del peralte.
	public float masa;
	public float velocidad; // Esta velocidad es para comparar con las de límite.

	public float mu;
	protected float angulo = 20f;
	protected PeralteProblem peralteProblem;

	public void init() {
		peralteProblem.solve ();
		construirFBDFuerzasReales();
		construirFBDXY();
		FBDActual = FBDFuerzasReales;
	}

	public abstract void construirFBDFuerzasReales();
	public abstract void construirFBDXY();

	public void SwitchFBD(Fbd diagrama) {
		if (FBDActual != null) {
			FBDActual.SetActive (false);
		}
		if (diagrama.Equals(Fbd.NormalTangencial)) {
//			FBDActual = FBDNormalTangencial;
			// Cancelled.
		} else if (diagrama.Equals(Fbd.XY)) {
			FBDActual = FBDXY;
		} else if (diagrama.Equals(Fbd.FuerzasReales)) {
			FBDActual = FBDFuerzasReales;
		} else {
			Debug.LogError("Error, Nombre de FBD desconocido");
		}
		FBDActual.SetActive(true);
	}

	public override void setActive(bool active) {
		if (FBDActual) {
			FBDActual.SetActive (active);
		}
	}
}
