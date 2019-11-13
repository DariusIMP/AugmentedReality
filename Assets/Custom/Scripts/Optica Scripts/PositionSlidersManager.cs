using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSlidersManager : MonoBehaviour
{

    public GameObject ConvergingLensSlider;


    public void ShowConvergingLens()
    {
        HideAll();
        ConvergingLensSlider.SetActive(true);
    }

    public void ShowConcaveMirror()
    {
        HideAll();
        //ConcaveMirrorSlider.SetActive(true);
    }

    public void HideAll()
    {
        ConvergingLensSlider.SetActive(false);
    }

}
