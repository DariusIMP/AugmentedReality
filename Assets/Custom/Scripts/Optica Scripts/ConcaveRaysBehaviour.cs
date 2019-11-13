using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcaveRaysBehaviour : RaysBehaviour
{

    private GameObject ParallelRay, CenterRay, FocalRay;
    private ConcaveMirrorBehaviour Mirror;


    public void Initialize(GameObject target, ConcaveMirrorBehaviour mirror)
    {
        this.Target = target;
        this.Mirror = mirror;

        ParallelRay = CreateRay("Parallel Ray");
        CenterRay = CreateRay("Center Ray");
        FocalRay = CreateRay("Focal Ray");

        PositionRays();
    }


    protected void PositionRays()
    {
        // Here we calculate where a ray parallel to the axis meets the mirror
        Vector3 mirrorCenter = Mirror.GetCenter();

        Vector3 parallelDirection = Mirror.transform.position - Target.transform.position;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, OriginPoint, parallelDirection)[0];

        Vector3 focalDirection = Mirror.GetFocalPoint() - OriginPoint;
        Vector3 focalHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, OriginPoint, focalDirection)[1];

        // Here we calculate the intersection between the rays so as to place the virtual image
        ConvergingPoint = CalculateLinesIntersection(
            parallelHit, Mirror.GetFocalPoint(), focalHit, focalHit + parallelDirection
        );

        // And now we set the rays points
        ParallelRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { OriginPoint, parallelHit, ConvergingPoint }
        );

        Vector3 centerHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, OriginPoint, OriginPoint - mirrorCenter)[0];
        CenterRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { mirrorCenter, ConvergingPoint }
        );
        
        FocalRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { OriginPoint, focalHit, ConvergingPoint }
        );
    }

}
