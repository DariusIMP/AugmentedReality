using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentationsManager : MonoBehaviour
{

    public GameObject PlaneMirror, ConvexMirror, ConvergingLens, ConcaveMirror;


    private void Start()
    {
        //DeactivateAll();
    }

    public void ActivatePlaneMirror()
    {
        DeactivateAll();
        PlaneMirror.SetActive(true);
    }

    public void ActivateConvexMirror()
    {
        DeactivateAll();
        ConvexMirror.SetActive(true);
    }

    public void ActivateConvergingLens()
    {
        DeactivateAll();
        ConvergingLens.SetActive(true);
    }

    public void ActivateConcaveMirror()
    {
        DeactivateAll();
        ConcaveMirror.SetActive(true);
    }


    private void DeactivateAll()
    {
        PlaneMirror.SetActive(false);
        ConvexMirror.SetActive(false);
        ConvergingLens.SetActive(false);
        ConcaveMirror.SetActive(false);
    }
}
