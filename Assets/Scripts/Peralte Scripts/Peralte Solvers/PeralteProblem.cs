using System;
using System.Collections;
using System.Collections.Generic;

public abstract class PeralteProblem {
	// Primero resolvemos el problema. Luego vemos qué hacemos con los diagramas.

	// Datos fijos para nuestro problema.
	public const float angulo = 20f / 180f * (float) Math.PI; // Hardcodeado.
	public const float R = 30; // Esto después lo ajustamos para una velocidad razonable.
	public const float g = 9.8f;

	// Variables del problema.
	public float masa;
	public float velocidad;
	public float mu;

	// Magnitudes calculadas.
	public float P;
	public float Normal;
	public float Nx;
	public float Ny;
	public float Fr;
	public float Frx;
	public float Fry;
	public float vLimite;
	public float fuerzaCentripeta;

	// Variables trigonometricas.
	protected float c;
	protected float s;
	protected float tan;

	public PeralteProblem(float masa, float velocidad, float mu) {
		this.masa = masa;
		this.velocidad = velocidad;
		this.mu = mu;

		c = (float) Math.Cos(angulo);
		s = (float) Math.Sin(angulo);
		tan = (float) Math.Tan (angulo);

		P = g*masa;
	}

	// Calcula el valor de todas las fuerzas para poder mostrarlas.
	public abstract void solve();
}