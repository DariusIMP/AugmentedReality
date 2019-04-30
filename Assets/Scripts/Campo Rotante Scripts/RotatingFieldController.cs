using UnityEngine;

namespace Campo_Rotante_Scripts
{
    public class RotatingFieldController : MonoBehaviour
    {
        public GameObject Arrow;
        public GameObject CentralObject;
        public GameObject PositiveCables, NegativeCables;
        public Material PCableMaterial, NCableMaterial;

        public void Flip()
        {
            if (Arrow.GetComponent<Spin>().speed < 0)
            {
                //Esto está re harcodeado pero bueno estoy trabajando de onda en un primero de enero, no me importa nada!
                Arrow.GetComponent<Spin>().speed *= -1.5f;
                //CentralObject.GetComponent<Spin>().speed *= -1f;
                Arrow.transform.rotation = new Quaternion(0, 0, 180, 0);
                ChangeMaterial(PositiveCables, PCableMaterial);
                ChangeMaterial(NegativeCables, NCableMaterial);
            }
            else
            {
                Arrow.GetComponent<Spin>().speed *= 1.5f;
                //CentralObject.GetComponent<Spin>().speed *= 1f;
                Arrow.transform.rotation = new Quaternion(0, 0, 0, 0);
                ChangeMaterial(PositiveCables, NCableMaterial);
                ChangeMaterial(NegativeCables, PCableMaterial);
            }
        }

        private void ChangeMaterial(GameObject cables, Material newMat)
        {
            Renderer[] children;
            children = cables.GetComponentsInChildren<Renderer>();
            foreach (Renderer rend in children)
            {
                var mats = new Material[rend.materials.Length];
                for (var j = 0; j < rend.materials.Length; j++)
                {
                    mats[j] = newMat;
                }
                rend.materials = mats;
            }
        }
    }
}
