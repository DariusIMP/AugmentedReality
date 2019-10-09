using UnityEngine;


public class ConvexMirrorBehaviour : IMirrorBehaviour
{

    public GameObject Target, VirtualImage;
    public ConvexRaysBehaviour RaysBehaviour;


    public void Start()
    {
        RaysBehaviour.Initialize(Target, this);
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

}
