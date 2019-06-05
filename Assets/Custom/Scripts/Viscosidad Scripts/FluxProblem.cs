using UnityEngine;
using System;

namespace Viscosidad_Scripts
{
	public class FluxProblem
	{
		// acceleration of gravity in m/(s^2).
		private const double g = 9.80665;

		// Diameter of the ball in meters.
		private double diameter;

		// Acceleration due to upthrust.
		private readonly double upthrustAccel;

		// Volume in cubic meters.
		private double volume;

		// Constant part of the drag force.
		private readonly double dragConstant;

		// Constant part of the reynolds number.
		private readonly double reynoldsConstant;

		// Acceleration due to drag.
		private double dragAccel;

		/** 
		 * Arguments: 
		 * Density of the flux in g/(m^3).
		 * Diameter of the ball in meters.
		 * Viscosity of the flux in Poises (Pa*s).
		 * Mass of the ball in kg;
		 */
		public FluxProblem (double density, double diameter, double viscosity, double mass) {
			volume = Math.PI * Math.Pow (diameter, 3) / 6;

			// The acceleration due to upthrust is constant.
			upthrustAccel = volume * density * g / mass;
			
			// density * area / 2m.
			dragConstant = density * Math.PI * Math.Pow(diameter, 2) / (8 * mass);
			reynoldsConstant = Math.Sqrt(24 * viscosity / (diameter * density));
		}
	
		/** 
		 * Acceleration with respect to velocity to build the differential equation.
		 * 
		 * The equation is composed of the three forces involved: weight (downwards), drag and upthrust (both upwards).
		 * 
		 * weight = g*m
		 * drag is governed by the drag equation: Fd = 1/2 density * v^2 * Cd * Area
		 * where Cd is related depends in a non linear way wit velocity (proportional to 1/v + 1/sqrt(v)).
		 */
		public double acceleration(double velocity)
		{
			return upthrustAccel + getDragAccel(velocity) - g;
		}

		public double getUpthrustAccel()
		{
			return upthrustAccel;
		}

		public double getDragAccel(double velocity)
		{
			return - dragConstant * Math.Pow (reynoldsConstant * Math.Sqrt (Math.Abs(velocity)) + Math.Abs(velocity) * 0.5407, 2) * Math.Sign (velocity);
		}
	}
}
