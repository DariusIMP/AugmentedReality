using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivergingLensBehaviour : MirrorBehaviour
{

    public DivergingRaysBehaviour RaysBehaviour;
    public float FocalDistance = 0.5f;


    void Start()
    {
        RaysBehaviour.Initialize(RealObject, this);
        PositionProjectedTarget(RaysBehaviour.OriginPoint);
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


    private void PositionProjectedTarget(Vector3 originPoint)
    {
        float imageScale = (RaysBehaviour.ConvergingPoint.y / originPoint.y) * RealObject.transform.localScale.y;

        ProjectedImage.transform.localScale = new Vector3(imageScale, imageScale, 1);
        ProjectedImage.transform.localPosition = new Vector3(0, 0, RaysBehaviour.ConvergingPoint.z);
    }

}
