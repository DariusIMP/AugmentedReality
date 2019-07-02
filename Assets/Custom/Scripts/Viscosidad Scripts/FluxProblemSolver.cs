using System;
using UnityEngine;

namespace Viscosidad_Scripts
{
    public class FluxProblemSolver
    {
        /** acceleration of gravity in m/(s^2). */
        private const double g = 9.80665;

        /** Minimum physical position (bottom of the tube) in meters. */
        private const double minPos = -1.60;
        
        /** The point in time at which the ball will enter the liquid. */
        private readonly double t0;

        /** The point in time after which the ball will be in the bottom of the tube. */
        private double tf;
        
        /** The point in space at which the ball starts falling, 0 being the surface of the liquid.*/
        private readonly double y0;

        private readonly FluxProblem fluxProblem;
        private Integration integ;
        private RK4 rk4;
        
        public FluxProblemSolver(FluxProblem fluxProblem, int n, double step, double y0)
        {
            this.fluxProblem = fluxProblem;
            this.y0 = y0;
            
            // Solve the problem.
            t0 = Math.Sqrt(2 * y0 / g);
            rk4 = new RK4 (step, t0, -t0*g, f, minPos);
            Debug.Log ("v(t) calculated");
		
            integ = new Integration (rk4.getResults(), 0, t0, step); 
            Debug.Log ("x(t) calculated");

            tf = step * rk4.getResults().Length + t0;
        }
        
        /** Function adaptor for the flux problem to enter it as a state equation for Runge Kutta 4. */
        private double f(double time, double velocity) {
            return fluxProblem.acceleration (velocity);
        }
        
        /** Returns the y given a t if the ball is in free fall. */
        private double freeFall(double t) 
        {
            return y0 - g * t * t / 2;
        }
        
        /** Gets the vertical position of the ball given the time. */
        public double getY(double time)
        {
            if (time > tf) return minPos;
            return time > t0 ? integ.getResultAt(time) : freeFall(time);
        }
        
        /** Gets vertical velocity for a given time. Negative velocity points downwards. */
        public double getVelocity(double time)
        {
            if (time > tf) return 0;
            return time < t0 ? -g * time : rk4.getResultAt(time);
        }
        
        /** Drag acceleration due to the viscosity of the liquid. */
        public double getDragAccel(double time)
        {
            // There's no drag force before entering the liquid.
            return time < t0 ? 0 : fluxProblem.getDragAccel(getVelocity(time));
        }

        /** Acceleration due to upthrust (Archimedes principle). */
        public double getUpthrustAccel(double time)
        {
            // There's no upthrust before entering the liquid.
            return time < t0 ? 0 : fluxProblem.getUpthrustAccel();
        }

        public double getGravityAccel()
        {
            return g;
        }
    }
}