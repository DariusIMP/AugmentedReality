using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentationsManager : MonoBehaviour
{

    public GameObject ConvexMirror, ConvergingLens, ConcaveMirror;


    private void Start()
    {
        DeactivateAll();
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
        ConvexMirror.SetActive(false);
        ConvergingLens.SetActive(false);
        ConcaveMirror.SetActive(false);
    }
}
