using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaysBehaviour : MonoBehaviour
{

    public Material RayMaterial, VirtualRayMaterial;
    public float RayWidth = 0.003f;

    public Vector3 ConvergingPoint { get; protected set; }

    protected GameObject Target;


    protected GameObject CreateRay(string name = "")
    {
        GameObject ray = new GameObject(name);
        ray.transform.SetParent(gameObject.transform, false);
        TubeRenderer renderer = ray.AddComponent<TubeRenderer>();
        renderer.material = RayMaterial;
        renderer.RadiusOne = RayWidth / 2;
        renderer.UseWorldSpace = false;
        renderer.Sides = 8;
        return ray;
    }

    protected GameObject CreateVirtualRay(string name = "")
    {
        GameObject ray = CreateRay(name);
        ray.GetComponent<TubeRenderer>().material = VirtualRayMaterial;
        return ray;
    }

    /**
     *  Calculates intersection between two lines, P and Q
     *  We assume the intersection exists
     */
    protected Vector3 CalculateLinesIntersection(Vector3 p1, Vector3 p2, Vector3 q1, Vector3 q2)
    {
        // We define line P as `alpha * (p2 - p1) + p1`, and line Q as `beta * (q2 - q1) + q1`
        //  being 'alpha' and 'beta' variable parameters, and then equalize both expressions
        float alpha;

        if (q1.y == q2.y)
        {
            // Special case: line Q is parallel to y-axis
            alpha = (q1.y - p1.y) / (p2.y - p1.y);
        }
        else
        {
            // Generic case
            alpha = ((p1.y - q1.y) * (q2.z - q1.z) / (q2.y - q1.y) + q1.z - p1.z) /
                (p2.z - p1.z - (p2.y - p1.y) * (q2.z - q1.z) / (q2.y - q1.y));
        }

        Vector3 intersection = alpha * (p2 - p1) + p1;
        return intersection;
    }

    /**
     *  Calculates intersection between given sphere and given line `alpha * direction + origin`
     *  We assume the intersection exists. Otherwise, Mathf.Sqrt(...) will throw an exception
     *  Source: https://en.wikipedia.org/wiki/Line%E2%80%93sphere_intersection
     */
    protected Vector3[] GetSphereLineIntersection(float radius, Vector3 center, Vector3 origin, Vector3 direction)
    {
        Vector3 unitDirection = direction.normalized;
        float b = 2 * (Vector3.Dot(unitDirection, (origin - center)));
        float c = (origin - center).sqrMagnitude - radius * radius;

        float radicand = b * b - 4 * c;
        float alphaPositive = (-b + Mathf.Sqrt(radicand)) / 2;
        float alphaNegative = (-b - Mathf.Sqrt(radicand)) / 2;
        return new Vector3[] { unitDirection * alphaPositive + origin, unitDirection * alphaNegative + origin };
    }

    /**
     * Calculates intersection between given plane `normal . (x,y,z) + d = 0`
     * and given line `alpha * direction + origin`
     * We assume the intersection exists
     */
     protected Vector3 GetPlaneLineIntersection(Vector3 normal, Vector3 planePoint, Vector3 origin, Vector3 direction)
    {
        Vector3 unitDirection = direction.normalized;
        Vector3 unitNormal = normal.normalized;

        float d = -Vector3.Dot(unitNormal, planePoint);
        float alpha = -(d + Vector3.Dot(unitNormal, origin)) / (Vector3.Dot(unitNormal, unitDirection));

        return unitDirection * alpha + origin;
    }

}
