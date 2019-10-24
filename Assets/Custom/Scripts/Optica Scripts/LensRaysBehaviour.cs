using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensRaysBehaviour : RaysBehaviour
{

    private GameObject CenterRay, TopRay, BottomRay;
    private GameObject Target;
    private ConvergingLensBehaviour Lens;

    void Start()
    {
        
    }

    public void Initialize(GameObject target, ConvergingLensBehaviour lens)
    {
        this.Target = target;
        this.Lens = lens;

        TopRay = CreateRay("TopRay");
        CenterRay = CreateRay("CenterRay");
        BottomRay = CreateRay("BottomRay");

        PositionRays();
    }


    private void PositionRays()
    {
        float objectHeight = 0.1f;
        Vector3 originPoint = Target.transform.position + new Vector3(0, objectHeight, 0);
        Vector3 planeNormal = Lens.GetPlaneNormal();
        Vector3 planeOffset = Lens.transform.position;

        Vector3 parallelDirection = planeOffset;
        parallelDirection.y = 0;
        Vector3 parallelHit = GetPlaneLineIntersection(planeNormal, planeOffset, originPoint, parallelDirection);
        TopRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { originPoint, parallelHit, Lens.GetFocusPosition() }
        );

        CenterRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { originPoint, originPoint, planeOffset }
        );

        Vector3 antifocusDirection = Lens.GetAntiFocusPosition() - originPoint;
        Vector3 antifocusHit = GetPlaneLineIntersection(planeNormal, planeOffset, originPoint, antifocusDirection);
        BottomRay.GetComponent<LineRenderer>().SetPositions(
            new Vector3[] { originPoint, antifocusHit, antifocusHit + parallelDirection }
        );
    }

}
