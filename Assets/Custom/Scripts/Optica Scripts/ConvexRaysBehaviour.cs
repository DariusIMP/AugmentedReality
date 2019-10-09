using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexRaysBehaviour : RaysBehaviour
{

    private GameObject parallelRay, centerRay, focalRay;
    private GameObject Target;
    private ConvexMirrorBehaviour Mirror;


    public void Initialize(GameObject target, ConvexMirrorBehaviour mirror)
    {
        this.Target = target;
        this.Mirror = mirror;

        parallelRay = CreateRay("Parallel Ray");
        centerRay = CreateRay("Center Ray", 2);
        focalRay = CreateRay("Focal Ray");

        PositionRays();     
    }

    public void Update()
    {
        PositionRays();
    }


    private void PositionRays()
    {
        // We will only show rays of the top point

        // We will asume the bottom point of 'target' is over the axis
        // We call 'targetPoint' to the point in the target from which we will show the rays
        float targetHeight = 0.25f;
        Vector3 targetPoint = Target.transform.position;
        targetPoint.y = targetHeight;

        // Here we calculate where a ray parallel to the axis meets the mirror
        Vector3 mirrorCenter = Mirror.GetCenter();
        float mirrorRadius = Mirror.GetRadius();
        Vector3 parallelDirection = Mirror.transform.position;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetSphereLineIntersection(mirrorRadius, mirrorCenter, targetPoint, parallelDirection);

        Vector3 focalPoint = Mirror.GetFocalPoint();
        parallelRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { targetPoint, parallelHit, focalPoint }
        );

        // Center ray is easy:
        centerRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { targetPoint, mirrorCenter }
        );

        // Here we calculate the intersection between the rays so as to place the virtual image
        Vector3 intersection = CalculateIntersection(
            parallelHit, Mirror.GetFocalPoint(), targetPoint, mirrorCenter
        );
        float virtualImgHeight = intersection.y - mirrorCenter.y;

        // And now we calculate the points for the ray projecting to the focal point
        Vector3 focalDirection = Mirror.GetFocalPoint() - targetPoint;
        Vector3 focalHit = GetSphereLineIntersection(mirrorRadius, mirrorCenter, targetPoint, focalDirection);
        focalRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { targetPoint, focalHit, intersection }
        );
    }

    /**
     *  Calculates intersection between given sphere and given line `alpha * direction + origin`
     *  We assume everything is contained on plane x=0
     *  Source: https://en.wikipedia.org/wiki/Line%E2%80%93sphere_intersection
     */
    private Vector3 GetSphereLineIntersection(float radius, Vector3 center, Vector3 origin, Vector3 direction)
    {
        Vector3 unitDirection = direction.normalized;
        float b = 2 * (Vector3.Dot(unitDirection, (origin - center)));
        float c = (origin - center).sqrMagnitude - radius * radius;

        float alpha = (-b - Mathf.Sqrt(b * b - 4 * c)) / 2;
        return unitDirection * alpha + origin;
    }

}
