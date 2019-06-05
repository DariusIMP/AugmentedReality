using System;
using UnityEngine;

public class PeralteConRozamientoVMax : PeralteProblem
{
	public PeralteConRozamientoVMax(float masa, float velocidad, float mu) : base(masa, velocidad, mu) {}

	public override void solve() {

		Normal = P / (c - mu * s);
		Nx = Normal*s;
		Ny = Normal*c;

		fuerzaCentripeta = Normal * (s + mu * c);

		Frx = fuerzaCentripeta - Nx;
		Fry = Ny - P;
		Fr = (float) Math.Sqrt (Math.Pow(Frx, 2) + Math.Pow(Fry, 2));
		Debug.Log ("mu: " + mu);
		Debug.Log ("Fr: " + Fr + " | Frx: " + Frx + " | Fry: " + Fry);		

		vLimite = (float) Math.Sqrt( R * g *( s + mu * c) / (c - mu * s));
	}
}