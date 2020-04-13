using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcaveRaysBehaviour : RaysBehaviour
{

    public Vector3 NearOriginPoint, FarOriginPoint;

    private GameObject ParallelRay, CenterRay, FocalRay;
    private GameObject ParallelVirtualRay, FocalVirtualRay;
    private ConcaveMirrorBehaviour Mirror;


    public void Initialize(GameObject target, ConcaveMirrorBehaviour mirror)
    {
        this.Target = target;
        this.Mirror = mirror;

        ParallelRay = CreateRay("Parallel Ray");
        CenterRay = CreateRay("Center Ray");
        CenterRay.SetActive(false); // Es medio confuso mostrarlo
        FocalRay = CreateRay("Focal Ray");

        ParallelVirtualRay = CreateVirtualRay("Parallel Virtual Ray");
        FocalVirtualRay = CreateVirtualRay("Focal Virtual Ray");

        PositionRaysForFarPosition();
    }


    public void PositionRaysForFarPosition()
    {
        // Here we calculate where a ray parallel to the axis meets the mirror
        Vector3 mirrorCenter = Mirror.GetCenter();

        Vector3 parallelDirection = Mirror.transform.position - Target.transform.position;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, FarOriginPoint, parallelDirection)[0];

        Vector3 focalDirection = Mirror.GetFocalPoint() - FarOriginPoint;
        Vector3 focalHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, FarOriginPoint, focalDirection)[0];

        // Here we calculate the intersection between the rays so as to place the virtual image
        ConvergingPoint = CalculateLinesIntersection(
            parallelHit, Mirror.GetFocalPoint(), focalHit, focalHit + parallelDirection
        );

        // And now we set the rays points
        ParallelRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { FarOriginPoint, parallelHit, 1.3f * (ConvergingPoint - parallelHit) + parallelHit }
        );
        
        Vector3 centerHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, FarOriginPoint, FarOriginPoint - mirrorCenter)[0];
        CenterRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { FarOriginPoint, ConvergingPoint }
        );
        
        FocalRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { FarOriginPoint, focalHit, 1.3f * (ConvergingPoint - focalHit) + focalHit }
        );

        ParallelVirtualRay.SetActive(false);
        FocalVirtualRay.SetActive(false);
    }


    public void PositionRaysForNearPosition()
    {
        // Here we calculate where a ray parallel to the axis meets the mirror
        Vector3 mirrorCenter = Mirror.GetCenter();

        Vector3 parallelDirection = Mirror.transform.localPosition - Target.transform.localPosition;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, NearOriginPoint, parallelDirection)[0];

        Vector3 focalDirection = Mirror.GetFocalPoint() - NearOriginPoint;
        Vector3 focalHit = GetSphereLineIntersection(Mirror.Radius, mirrorCenter, NearOriginPoint, focalDirection)[1];

        // Here we calculate the intersection between the rays so as to place the virtual image
        ConvergingPoint = CalculateLinesIntersection(
            parallelHit, Mirror.GetFocalPoint(), focalHit, focalHit + parallelDirection
        );

        // And now we set the rays points
        ParallelRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { NearOriginPoint, parallelHit, Mirror.GetFocalPoint() }
        );

        CenterRay.SetActive(false);

        FocalRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { NearOriginPoint, focalHit, focalHit - parallelDirection }
        );

        ParallelVirtualRay.SetActive(true);
        ParallelVirtualRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { parallelHit, ConvergingPoint }
        );

        Vector3 centerHit = GetSphereLineIntersection(
            Mirror.Radius, mirrorCenter, NearOriginPoint, mirrorCenter
        )[1];

        FocalVirtualRay.SetActive(true);
        FocalVirtualRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { focalHit, focalHit + parallelDirection }
        );
    }

}
