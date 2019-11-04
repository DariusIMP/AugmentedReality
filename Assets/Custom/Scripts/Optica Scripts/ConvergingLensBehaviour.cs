using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvergingLensBehaviour : IMirrorBehaviour
{

    public LensRaysBehaviour RaysBehaviour;
    public float FocalDistance = 0.5f;


    void Start()
    {
        RaysBehaviour.Initialize(RealObject, this);
        PositionVirtualTarget();
    }

    public Vector3 GetFocusPosition()
    {
        return transform.localPosition + new Vector3(0, 0, FocalDistance);
    }

    public Vector3 GetAntiFocusPosition()
    {
        return transform.localPosition - new Vector3(0, 0, FocalDistance);
    }

    public Vector3 GetPlaneNormal()
    {
        return transform.forward;
    }


    private void PositionVirtualTarget()
    {
        float scaleMagnitude = RaysBehaviour.ConvergingPoint.y / RaysBehaviour.OriginPoint.y;
        Vector3 tgtScale = RealObject.transform.localScale;
        tgtScale.y *= scaleMagnitude;
        tgtScale.x *= Mathf.Abs(scaleMagnitude);
        VirtualImage.transform.localScale = tgtScale;
        VirtualImage.transform.localPosition.Set(0, 0, RaysBehaviour.ConvergingPoint.z);
    }

}
