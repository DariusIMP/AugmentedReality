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
        Vector3 position = transform.position;
        return position + new Vector3(0, 0, radius);
    }

    public Vector3 GetFocalPoint()
    {
        float radius = transform.localScale.z;
        Vector3 position = transform.position;
        return position + new Vector3(0, 0, radius / 2);
    }

}
