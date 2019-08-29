using System;
using UnityEngine;

namespace Custom.Scripts.OsciloscopioScripts
{
    public class SpinKnob : MonoBehaviour
    {
        
        public void Spin(float variation)
        {
            transform.Rotate(0, variation * 2 * 3.14f, 0);
        }
    }
}
