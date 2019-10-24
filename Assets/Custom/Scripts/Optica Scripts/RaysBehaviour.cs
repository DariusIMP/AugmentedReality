using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaysBehaviour : MonoBehaviour
{

    public Material RayMaterial;

    private const float RAY_WIDTH = 0.005f;


    protected GameObject CreateRay(string name = "", int points = 3)
    {
        GameObject ray = new GameObject(name);
        ray.transform.parent = gameObject.transform;
        LineRenderer renderer = ray.AddComponent<LineRenderer>();
        renderer.positionCount = points;
        renderer.material = RayMaterial;
        renderer.startWidth = RAY_WIDTH;
        renderer.useWorldSpace = false;
        return ray;
    }

    /**
     *  Calculates intersection between segment P and Q
     *  We will only assume q1.y != q2.y and that the intersection exists
     */
    protected Vector3 CalculateIntersection(Vector3 p1, Vector3 p2, Vector3 q1, Vector3 q2)
    {
        // The methodology is the following: line P is defined by `alpha * (p2 - p1) + p1`,
        //  and line Q by `beta * (q2 - q1) + q1` being 'alpha' and 'beta' variable parameters
        // If we equalize both expressions we get `alpha * (p2.y - p1.y) + p1.y == beta * (q2.y -
        //  q1.y) + q1.y`, and the same for the x-axis and z-axis. We can then express beta
        //  in terms of alpha, and  replace it in the z-axis equation. By now we know the value
        //  of alpha, so we finally replace it at the P line definition and we get the intersection. 
        // This intersection point should also be contained in the Q line, and we assume it is

        float alpha = ((p1.y - q1.y) * (q2.z - q1.z) / (q2.y - q1.y) + q1.z - p1.z) /
                (p2.z - p1.z - (p2.y - p1.y) * (q2.z - q1.z) / (q2.y - q1.y));

        Vector3 intersection = alpha * (p2 - p1) + p1;
        return intersection;
    }

    /**
     *  Calculates intersection between given sphere and given line `alpha * direction + origin`
     *  We assume the intersection exists. Otherwise, Mathf.Sqrt(...) will throw an exception
     *  Source: https://en.wikipedia.org/wiki/Line%E2%80%93sphere_intersection
     */
    protected Vector3 GetSphereLineIntersection(float radius, Vector3 center, Vector3 origin, Vector3 direction)
    {
        Vector3 unitDirection = direction.normalized;
        float b = 2 * (Vector3.Dot(unitDirection, (origin - center)));
        float c = (origin - center).sqrMagnitude - radius * radius;

        float alpha = (-b - Mathf.Sqrt(b * b - 4 * c)) / 2;
        return unitDirection * alpha + origin;
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
