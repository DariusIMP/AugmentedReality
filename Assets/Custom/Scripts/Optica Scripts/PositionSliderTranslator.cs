using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSliderTranslator : MonoBehaviour
{

    public ConvergingLensBehaviour Lens;
    

    public void OnValueChanged(float value)
    {
        if (value == 0)
        {
            Lens.PositionFarFromLens();
        }
        else if (value == 1)
        {
            Lens.PositionNearFromLens();
        }
    }

}
