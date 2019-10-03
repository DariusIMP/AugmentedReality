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
        float distance = transform.position.z;
        //ReflectionImage.transform.localScale.Set(1, 1, 1);
        ReflectionImage.transform.position.Set(0, 0, 2 * distance);
        RaysBehaviour.Initialize(Target, ReflectionImage);

        //Vector3 mirrorPosition = mirrorObject.transform.position;
        //mirrorPosition.z = distance;
        //mirrorObject.transform.position = mirrorPosition;
    }

    public void Update()
    {
        //float distance = transform.position.z;
        //Vector3 reflectionPosition = Target.transform.position;
        //reflectionPosition.z = 2 * distance;
        //ReflectionImage.transform.position = reflectionPosition;
    }
    
}
