using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivergingRaysBehaviour : RaysBehaviour
{

    public Vector3 OriginPoint;

    private GameObject ParallelRay, FocalRay;
    private GameObject ParallelVirtualRay, AntifocalVirtualRay;
    private DivergingLensBehaviour Lens;


    public void Initialize(GameObject target, DivergingLensBehaviour lens)
    {
        this.Target = target;
        this.Lens = lens;

        ParallelRay = CreateRay("Parallel Ray");
        FocalRay = CreateRay("Focal Ray");

        ParallelVirtualRay = CreateVirtualRay("Parallel Virtual Ray");
        AntifocalVirtualRay = CreateVirtualRay("Antifocal Virtual Ray");

        PositionRays();
    }

    public void PositionRays()
    {
        Vector3 planeNormal = Lens.GetPlaneNormal();
        Vector3 lensPosition = Lens.transform.localPosition;

        Vector3 parallelDirection = lensPosition - Target.transform.localPosition;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetPlaneLineIntersection(planeNormal, lensPosition, OriginPoint, parallelDirection);

        Vector3 focusDirection = Lens.GetFocusPosition() - OriginPoint;
        Vector3 focusHit = GetPlaneLineIntersection(planeNormal, lensPosition, OriginPoint, focusDirection);

        ConvergingPoint = CalculateLinesIntersection(
            parallelHit, Lens.GetAntiFocusPosition(), focusHit, focusHit + parallelDirection
        );

        ParallelRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { OriginPoint, parallelHit, 0.5f * (parallelHit - Lens.GetAntiFocusPosition()) + parallelHit }
        );

        FocalRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { OriginPoint, focusHit, focusHit + parallelDirection * 0.5f }
        );

        ParallelVirtualRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { focusHit, focusHit - parallelDirection }
        );

        AntifocalVirtualRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { parallelHit, Lens.GetAntiFocusPosition() }
        );
    }

}
