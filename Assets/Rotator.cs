using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Rotates the screen to vertical orientation before rendering starts.
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
}
