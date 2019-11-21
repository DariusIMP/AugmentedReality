using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcaveMirrorBehaviour : MirrorBehaviour
{

    public ConcaveRaysBehaviour RaysBehaviour;
    public TextMesh PositionText;
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
        PositionVirtualImage(RaysBehaviour.FarOriginPoint);
        PositionText.text = string.Format("{0}cm", (int)Mathf.Round(RaysBehaviour.FarOriginPoint.z * -100));
    }


    public void PositionFarFromLens()
    {
        RaysBehaviour.PositionRaysForFarPosition();
        PositionVirtualImage(RaysBehaviour.FarOriginPoint);
        RealObject.transform.localPosition = new Vector3(0, 0, RaysBehaviour.FarOriginPoint.z);
        PositionText.text = string.Format("{0}cm", (int)Mathf.Round(RaysBehaviour.FarOriginPoint.z * -100));
    }

    public void PositionNearFromLens()
    {
        RaysBehaviour.PositionRaysForNearPosition();
        PositionVirtualImage(RaysBehaviour.NearOriginPoint);
        RealObject.transform.localPosition = new Vector3(0, 0, RaysBehaviour.NearOriginPoint.z);
        PositionText.text = string.Format("{0}cm", (int)Mathf.Round(RaysBehaviour.NearOriginPoint.z * -100));
    }


    private void PositionVirtualImage(Vector3 originPoint)
    {
        Vector3 convergingPoint = RaysBehaviour.ConvergingPoint;
        float imageScale = (convergingPoint.y / originPoint.y) * RealObject.transform.localScale.y;

        Vector3 targetPos = convergingPoint;
        targetPos.y = 0;

        ProjectedImage.transform.localScale = new Vector3(imageScale, imageScale, 1);
        ProjectedImage.transform.localPosition = targetPos;
    }

}
