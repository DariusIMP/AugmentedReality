using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RaysBehaviour : MonoBehaviour
{

    public Material RayMaterial;

    private const float RAY_WIDTH = 0.005f;


    protected GameObject CreateRay(int points = 3)
    {
        GameObject ray = new GameObject();
        ray.transform.parent = gameObject.transform;
        LineRenderer renderer = ray.AddComponent<LineRenderer>();
        renderer.positionCount = points;
        renderer.material = RayMaterial;
        renderer.startWidth = RAY_WIDTH;
        return ray;
    }

    /**
     *  Calculates intersection between segment A and B
     *  We will asume everything is in plane x=0
     */
    protected Vector3 CalculateIntersection(Vector3 p1A, Vector3 p2A, Vector3 p1B, Vector3 p2B)
    {
        // We define segment A is defined by equation `y = mA * z + bA`, and the same with segment B
        float mA = (p1A.y - p2A.y) / (p1A.z - p2A.z);
        float mB = (p1B.y - p2B.y) / (p1B.z - p2B.z);
        float bA = p1A.y - mA * p1A.z;
        float bB = p1B.y - mA * p1B.z;

        float intersectionZ = (bB - bA) / (mA - mB);
        float intersectionY = mA * intersectionZ + bA;

        return new Vector3(0, intersectionY, intersectionZ);
    }

}
