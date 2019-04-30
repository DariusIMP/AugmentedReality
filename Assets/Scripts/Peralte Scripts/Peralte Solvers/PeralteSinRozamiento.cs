using System;

public class PeralteSinRozamiento : PeralteProblem
{
	public PeralteSinRozamiento(float masa, float velocidad, float mu) : base(masa, velocidad, mu) {}

	public override void solve() {
		
		Normal = P / c;
		Nx = P * tan;
		Ny = P;

		Fr = 0;
		Frx = 0;
		Fry = 0;

		fuerzaCentripeta = Nx;
		vLimite = (float) Math.Sqrt(g*R*tan);
	}
}