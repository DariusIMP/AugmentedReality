using UnityEngine;


public class ConvexMirrorBehaviour : IMirrorBehaviour
{

    public GameObject Target, VirtualImage;
    public ConvexRaysBehaviour RaysBehaviour;


    public void Start()
    {
        RaysBehaviour.Initialize(Target, this);
        PositionVirtualImage();
    }

    public Vector3 GetCenter()
    {
        float radius = transform.localScale.z;
        Vector3 center = transform.position;
        center.y = 0;
        center = radius * center.normalized + transform.position;
        return center;
    }

    public Vector3 GetFocalPoint()
    {
        float radius = transform.localScale.z;
        Vector3 focal = transform.position;
        focal.y = 0;
        focal = radius / 2 * focal.normalized + transform.position;
        return focal;
    }

    public float GetRadius()
    {
        return transform.localScale.z;
    }


    private void PositionVirtualImage()
    {
        Vector3 topPoint = RaysBehaviour.GetConvergingPoint();
        float imageHeight = topPoint.y - transform.position.y;

        Vector3 targetPos = topPoint;
        targetPos.y -= imageHeight / 2;

        VirtualImage.transform.localScale = new Vector3(imageHeight * 2, imageHeight * 2, imageHeight * 2);
        VirtualImage.transform.position = targetPos;
    }

}
