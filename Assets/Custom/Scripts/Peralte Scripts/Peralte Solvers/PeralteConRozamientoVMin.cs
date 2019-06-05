using System;

public class PeralteConRozamientoVMin : PeralteProblem
{
	public PeralteConRozamientoVMin(float masa, float velocidad, float mu) : base(masa, velocidad, mu) {}

	public override void solve() {

		Normal = P / (c + mu * s);
		Nx = Normal*s;
		Ny = Normal*c;

		fuerzaCentripeta = Normal * (s - mu * c);

		Frx = Nx - fuerzaCentripeta;
		Fry = P - Ny;
		Fr = (float) Math.Sqrt (Math.Pow(Frx, 2) + Math.Pow(Fry, 2));

		vLimite = (float) Math.Sqrt( R * g *( s - mu * c) / (c + mu * s));
	}
}