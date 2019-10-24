using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvergingLensBehaviour : IMirrorBehaviour
{

    public GameObject Target;
    //public GameObject VirtualTarget;
    public GameObject Focus, AntiFocus;
    public LensRaysBehaviour RaysBehaviour;
    //public float FocalDistance = 0.5f;

    void Start()
    {
        //Focus.transform.position = transform.position + new Vector3(0, 0, FocalDistance);
        //AntiFocus.transform.position = transform.position + new Vector3(0, 0, -FocalDistance);
        RaysBehaviour.Initialize(Target, this);
    }

    public Vector3 GetFocusPosition()
    {
        return Focus.transform.position;
    }

    public Vector3 GetAntiFocusPosition()
    {
        return AntiFocus.transform.position;
    }

    public Vector3 GetPlaneNormal()
    {
        return transform.forward;
    }

}
