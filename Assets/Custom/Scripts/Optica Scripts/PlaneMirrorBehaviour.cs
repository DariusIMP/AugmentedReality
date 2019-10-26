using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMirrorBehaviour : IMirrorBehaviour
{
    
    public GameObject ReflectionImage;
    public PlaneRaysBehaviour RaysBehaviour;


    void Start()
    {
        float yOffset = 0.125f;
        Vector3 reflectionPos = 2 * (transform.position - RealObject.transform.position) + new Vector3(0, yOffset, 0);
        ReflectionImage.transform.position = reflectionPos;
        RaysBehaviour.Initialize(RealObject, this);
    }
    
}
