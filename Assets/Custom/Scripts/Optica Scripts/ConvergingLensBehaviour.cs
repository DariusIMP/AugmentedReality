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
        float imageScale = (RaysBehaviour.ConvergingPoint.y / RaysBehaviour.OriginPoint.y) * RealObject.transform.localScale.y;
        
        VirtualImage.transform.localScale = new Vector3(imageScale, Mathf.Abs(imageScale), 1);
        VirtualImage.transform.localPosition = new Vector3(0, 0, RaysBehaviour.ConvergingPoint.z);
    }

}
