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
        PositionProjectedTarget(RaysBehaviour.FarOriginPoint);
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

    public void PositionFarFromLens()
    {
        RaysBehaviour.PositionRaysForFarPosition();
        PositionProjectedTarget(RaysBehaviour.NearOriginPoint);
        RealObject.transform.localPosition = new Vector3(0, 0, RaysBehaviour.FarOriginPoint.z);
    }

    public void PositionNearFromLens()
    {
        RaysBehaviour.PositionRaysForNearPosition();
        PositionProjectedTarget(RaysBehaviour.FarOriginPoint);
        RealObject.transform.localPosition = new Vector3(0, 0, RaysBehaviour.NearOriginPoint.z);
    }


    private void PositionProjectedTarget(Vector3 originPoint)
    {
        float imageScale = (RaysBehaviour.ConvergingPoint.y / originPoint.y) * RealObject.transform.localScale.y;
        
        ProjectedImage.transform.localScale = new Vector3(imageScale, imageScale, 1);
        ProjectedImage.transform.localPosition = new Vector3(0, 0, RaysBehaviour.ConvergingPoint.z);
    }

}
