
using UnityEngine;

namespace Custom.Scripts.CampoRotanteScripts
{
    public class PhaseSum : MonoBehaviour
    {
        private Vector3 sumVectorPos;
        
        public GameObject FaseR, FaseS, FaseT;

        private void Start()
        {
            transform.forward = new Vector3(0,0,1);
        }
        
        private void Update()
        {
            sumVectorPos = FaseR.GetComponent<ArrowOscilator>().GetHeadPosition()
                + FaseS.GetComponent<ArrowOscilator>().GetHeadPosition() 
                + FaseT.GetComponent<ArrowOscilator>().GetHeadPosition();

            transform.LookAt(sumVectorPos, Vector3.up);
        }
    }
}
