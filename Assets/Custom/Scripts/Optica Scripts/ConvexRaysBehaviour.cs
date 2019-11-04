using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexRaysBehaviour : RaysBehaviour
{

    public Vector3 ConvergentPoint { get; private set; }

    private GameObject ParallelRay, CenterRay, FocalRay;
    private ConvexMirrorBehaviour Mirror;


    public void Initialize(GameObject target, ConvexMirrorBehaviour mirror)
    {
        this.Target = target;
        this.Mirror = mirror;

        ParallelRay = CreateRay("Parallel Ray");
        CenterRay = CreateRay("Center Ray", 2);
        FocalRay = CreateRay("Focal Ray");

        PositionRays();     
    }


    private void PositionRays()
    {
        // Here we calculate where a ray parallel to the axis meets the mirror
        Vector3 mirrorCenter = Mirror.GetCenter();
        Vector3 parallelDirection = Mirror.transform.position - Target.transform.position;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, OriginPoint, parallelDirection)[1];

        Vector3 focalPoint = Mirror.GetFocalPoint();
        ParallelRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { OriginPoint, parallelHit, focalPoint }
        );

        // Center ray is easy:
        CenterRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { OriginPoint, mirrorCenter }
        );

        // Here we calculate the intersection between the rays so as to place the virtual image
        ConvergentPoint = CalculateLinesIntersection(
            parallelHit, Mirror.GetFocalPoint(), OriginPoint, mirrorCenter
        );
        float virtualImgHeight = ConvergentPoint.y - mirrorCenter.y;

        // And now we calculate the points for the ray projecting to the focal point
        Vector3 focalDirection = Mirror.GetFocalPoint() - OriginPoint;
        Vector3 focalHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, OriginPoint, focalDirection)[1];
        FocalRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { OriginPoint, focalHit, ConvergentPoint }
        );
    }

}
