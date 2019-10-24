using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMirrorBehaviour : MonoBehaviour
{
    
    public GameObject Target;
    public GameObject ReflectionImage;
    public PlaneRaysBehaviour RaysBehaviour;


    void Start()
    {
        float yOffset = 0.125f;
        Vector3 reflectionPos = 2 * (transform.position - Target.transform.position) + new Vector3(0, yOffset, 0);
        ReflectionImage.transform.position = reflectionPos;
        RaysBehaviour.Initialize(Target, this);
    }
    
}
