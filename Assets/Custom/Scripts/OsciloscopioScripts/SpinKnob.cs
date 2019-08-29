using System;
using UnityEngine;

namespace Custom.Scripts.OsciloscopioScripts
{
    public class SpinKnob : MonoBehaviour
    {
   

        public void Spin(float variation)
        {
            Quaternion rotation = Quaternion.Euler(new Vector4(0, variation * 360, 0, 0));
            transform.rotation = rotation;
        }
    }
}
