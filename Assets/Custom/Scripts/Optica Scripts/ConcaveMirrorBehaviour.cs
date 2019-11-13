using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcaveMirrorBehaviour : IMirrorBehaviour
{

    public ConcaveRaysBehaviour RaysBehaviour;
    public float Radius;


    public Vector3 GetCenter()
    {
        Vector3 center = transform.localPosition;
        center.z -= Radius;
        return center;
    }

    public Vector3 GetFocalPoint()
    {
        Vector3 focal = transform.localPosition;
        focal.z -= Radius / 2;
        return focal;
    }


    private void Start()
    {
        RaysBehaviour.Initialize(RealObject, this);
        PositionVirtualImage();
    }

    private void PositionVirtualImage()
    {
        Vector3 convergingPoint = RaysBehaviour.ConvergingPoint;
        float imageScale = (convergingPoint.y / RaysBehaviour.OriginPoint.y) * RealObject.transform.localScale.y;

        Vector3 targetPos = convergingPoint;
        targetPos.y = 0;

        ProjectedImage.transform.localScale = new Vector3(imageScale, imageScale, 1);
        ProjectedImage.transform.localPosition = targetPos;
    }

}
