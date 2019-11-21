using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcaveMirrorPositionTranslator : MonoBehaviour
{
    public ConcaveMirrorBehaviour Mirror;


    public void OnValueChanged(float value)
    {
        if (value == 0)
        {
            Mirror.PositionFarFromLens();
        }
        else if (value == 1)
        {
            Mirror.PositionNearFromLens();
        }
    }

}
