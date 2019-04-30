using UnityEngine;

/** Integration of discrete functions in the form of an array. */
namespace Viscosidad_Scripts
{
	public class Integration {
		// Cache were results will be precalculated.
		double[] results;

		// Function to integrate.
		double[] func;

		// Amounts of members to evaluate.
		double n;
		
		/** Time difference between each calculation. */
		double step;
		
		/** Time for the initial solution */
		private double t0;
		
		public Integration (double[] func, double initialValue, double initialTime, int n, double step) {
			this.func = func;
			this.n = n;
			this.step = step;
			t0 = initialTime;
			results = new double[n];
			results [0] = initialValue;
			solve ();
		}

		public void solve() {
			// Most basic numeric integration: quadrature method.
			for (int i = 1; i < n; ++i) {
				results [i] = results [i - 1] + func [i] * step;
			}
		}

		/**
		 * Gets y(t') being t' the closest available time to @time. If the results requested are out of the solved
		 * interval, the frontier values are returned.
		 */
		public double getResultAt(double t) {
			int position = (int) ((t-t0) / step);
			if (position >= n) return results[position - 1];
			if (position < 0) return results[0];
			return results[position];
		}
	}
}


