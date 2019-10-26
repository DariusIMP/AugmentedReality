using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexRaysBehaviour : RaysBehaviour
{

    private GameObject ParallelRay, CenterRay, FocalRay;
    private GameObject Target;
    private ConvexMirrorBehaviour Mirror;
    private Vector3 ConvergentPoint;


    public void Initialize(GameObject target, ConvexMirrorBehaviour mirror)
    {
        this.Target = target;
        this.Mirror = mirror;

        ParallelRay = CreateRay("Parallel Ray");
        CenterRay = CreateRay("Center Ray", 2);
        FocalRay = CreateRay("Focal Ray");

        PositionRays();     
    }

    public void Update()
    {
        //PositionRays();
    }

    public Vector3 GetConvergingPoint()
    {
        return ConvergentPoint;
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
        ParallelRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { targetPoint, parallelHit, focalPoint }
        );

        // Center ray is easy:
        CenterRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { targetPoint, mirrorCenter }
        );

        // Here we calculate the intersection between the rays so as to place the virtual image
        ConvergentPoint = CalculateLinesIntersection(
            parallelHit, Mirror.GetFocalPoint(), targetPoint, mirrorCenter
        );
        float virtualImgHeight = ConvergentPoint.y - mirrorCenter.y;

        // And now we calculate the points for the ray projecting to the focal point
        Vector3 focalDirection = Mirror.GetFocalPoint() - targetPoint;
        Vector3 focalHit = GetSphereLineIntersection(mirrorRadius, mirrorCenter, targetPoint, focalDirection);
        FocalRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { targetPoint, focalHit, ConvergentPoint }
        );
    }

}
