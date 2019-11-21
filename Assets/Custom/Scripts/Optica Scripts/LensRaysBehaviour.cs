using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensRaysBehaviour : RaysBehaviour
{

    public Vector3 NearOriginPoint, FarOriginPoint;

    private GameObject CenterRay, ParallelRay, AntiFocalRay;
    private GameObject ParallelVirtualRay, CenterVirtualRay, AntiFocalVirtualRay;
    private ConvergingLensBehaviour Lens;


    public void Initialize(GameObject target, ConvergingLensBehaviour lens)
    {
        this.Target = target;
        this.Lens = lens;

        ParallelRay = CreateRay("Parallel Ray");
        CenterRay = CreateRay("Center Ray");
        AntiFocalRay = CreateRay("Antifocal Ray");

        ParallelVirtualRay = CreateVirtualRay("Parallel Virtual Ray");
        CenterVirtualRay = CreateVirtualRay("Center Virtual Ray");
        AntiFocalVirtualRay = CreateVirtualRay("Antifocal Virtual Ray");

        PositionRaysForFarPosition();
    }


    public void PositionRaysForFarPosition()
    {
        Vector3 planeNormal = Lens.GetPlaneNormal();
        Vector3 lensPosition = Lens.transform.localPosition;

        Vector3 parallelDirection = lensPosition - Target.transform.localPosition;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetPlaneLineIntersection(planeNormal, lensPosition, FarOriginPoint, parallelDirection);

        Vector3 antifocusDirection = Lens.GetAntiFocusPosition() - FarOriginPoint;
        Vector3 antifocusHit = GetPlaneLineIntersection(planeNormal, lensPosition, FarOriginPoint, antifocusDirection);

        ConvergingPoint = CalculateLinesIntersection(
            parallelHit, Lens.GetFocusPosition(), antifocusHit, antifocusHit + parallelDirection
        );

        ParallelRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { FarOriginPoint, parallelHit, ConvergingPoint }
        );

        CenterRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { FarOriginPoint, ConvergingPoint }
        );

        AntiFocalRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { FarOriginPoint, antifocusHit, ConvergingPoint }
        );

        ParallelVirtualRay.SetActive(false);
        AntiFocalVirtualRay.SetActive(false);
        CenterVirtualRay.SetActive(false);
    }


    public void PositionRaysForNearPosition()
    {
        Vector3 planeNormal = Lens.GetPlaneNormal();
        Vector3 lensPosition = Lens.transform.localPosition;

        Vector3 parallelDirection = lensPosition - Target.transform.localPosition;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetPlaneLineIntersection(planeNormal, lensPosition, NearOriginPoint, parallelDirection);

        Vector3 antifocusDirection = Lens.GetAntiFocusPosition() - NearOriginPoint;
        Vector3 antifocusHit = GetPlaneLineIntersection(planeNormal, lensPosition, NearOriginPoint, antifocusDirection);

        ConvergingPoint = CalculateLinesIntersection(
            parallelHit, Lens.GetFocusPosition(), antifocusHit, antifocusHit + parallelDirection
        );


        ParallelRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { NearOriginPoint, parallelHit, Lens.GetFocusPosition() }
        );

        CenterRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { NearOriginPoint, lensPosition }
        );

        AntiFocalRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { NearOriginPoint, antifocusHit, antifocusHit + parallelDirection * 0.5f }
        );

        ParallelVirtualRay.SetActive(true);
        AntiFocalVirtualRay.SetActive(true);
        CenterVirtualRay.SetActive(true);

        ParallelVirtualRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { antifocusHit, ConvergingPoint }
        );

        AntiFocalVirtualRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { parallelHit, ConvergingPoint }
        );

        CenterVirtualRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { NearOriginPoint, ConvergingPoint }
        );
    }

}
