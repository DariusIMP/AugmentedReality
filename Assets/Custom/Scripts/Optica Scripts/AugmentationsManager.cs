using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentationsManager : MonoBehaviour
{

    public GameObject PlaneMirror, ConvexMirror;


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

    private void DeactivateAll()
    {
        PlaneMirror.SetActive(false);
        ConvexMirror.SetActive(false);
    }
}
