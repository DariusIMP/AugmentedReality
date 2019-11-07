using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensRaysBehaviour : RaysBehaviour
{

    public Vector3 ConvergingPoint { get; private set; }

    private GameObject CenterRay, ParallelRay, AntiFocalRay;
    private ConvergingLensBehaviour Lens;


    public void Initialize(GameObject target, ConvergingLensBehaviour lens)
    {
        this.Target = target;
        this.Lens = lens;

        ParallelRay = CreateRay("Parallel Ray");
        CenterRay = CreateRay("Center Ray");
        AntiFocalRay = CreateRay("Antifocal Ray");

        PositionRays();
    }


    override protected void PositionRays()
    {
        Vector3 planeNormal = Lens.GetPlaneNormal();
        Vector3 lensPosition = Lens.transform.localPosition;

        Vector3 parallelDirection = lensPosition - Target.transform.localPosition;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetPlaneLineIntersection(planeNormal, lensPosition, OriginPoint, parallelDirection);

        Vector3 antifocusDirection = Lens.GetAntiFocusPosition() - OriginPoint;
        Vector3 antifocusHit = GetPlaneLineIntersection(planeNormal, lensPosition, OriginPoint, antifocusDirection);

        ConvergingPoint = CalculateConvergingPoint(parallelHit, antifocusHit.y);

        ParallelRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { OriginPoint, parallelHit, ConvergingPoint }
        );

        CenterRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { OriginPoint, ConvergingPoint }
        );

        AntiFocalRay.GetComponent<TubeRenderer>().SetPositions(
            new Vector3[] { OriginPoint, antifocusHit, ConvergingPoint }
        );
    }

    private Vector3 CalculateConvergingPoint(Vector3 parallelHit, float height)
    {
        Vector3 focalPoint = Lens.GetFocusPosition();
        float alpha = (height - parallelHit.y) / (focalPoint.y - parallelHit.y);
        return (focalPoint - parallelHit) * alpha + parallelHit;
    }

}
