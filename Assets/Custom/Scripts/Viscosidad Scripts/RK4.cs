using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Viscosidad_Scripts
{
	public class RK4 {

		// Result cache to calculate the derivatives when starting.
		private double[] results;
		private readonly int n;
		private readonly double h;
		private readonly double y0;
		private readonly double t0;
		private readonly Func<double, double, double> f;
	
		/**
		 * Builds and solves a differential equation in one variable.
		 * 
		 * The function wanted is y(t), and the differential equation provided is in the form y'(t) = f(t, y).
		 * 
		 * Arguments:
		 * n = amount of time steps.
		 * h = time step length.
		 * y0 = y(t0). Initial value for the variable wanted.
		 * t0 = time at which the process starts.
		 * f = The function that calculates the derivative given the time and the value of y.
		 */
		public RK4(int n, double h, double t0, double y0, Func<double, double, double> f) {
			this.n = n;
			this.f = f;
			this.h = h;
			this.y0 = y0;
			this.t0 = t0;
			solve();
		}
	
		/* Gets the next value using the current one, the time and the evaluator function f for the derivative. */
		private double next(double t, double w) {
			double k1 = h * f (t, w);
			double k2 = h * f (t + h / 2, w + k1/2);
			double k3 = h * f (t + h / 2, w + k2/2);
			double k4 = h * f (t + h / 2, w + k3);
			return w + (k1 + 2*k2 + 2*k3 + k4)/6;
		}
	
		/** Calculates and caches the results of the differential equation. */
		private void solve() {
			
			// Discrete time.
			double t = 0;
			// Discrete approximations to y.
			double w = y0;
	
			results = new double[n];
			for (int i = 0; i < n; i++) {
				w = next (t, w);
				t += h;
				results [i] = w;
			}
		}
	
		/** Gets y(t') being t' the closest available time to t. */
		public double getResultAt(double t) {
			int position = (int) ((t - t0) / h);
			if (position >= n || position < 0) {
				Debug.LogError ("Results requested for a time outside the calculated range: " + t);
			}
			if (position >= n) position = n - 1;
			if (position < 0) position = 0;
			return results [position];
		}
	
		/** Gets the results in double[n] array. */
		public double[] getResults() {
			return results;
		}
	}
}
