using UnityEngine;
using UnityEngine.UI;

namespace Campo_Rotante_Scripts
{
    public class RotatingFieldController : MonoBehaviour
    {
        public GameObject VectorFaseS, VectorFaseT, VectorFaseR;
        public GameObject CentralObject;
        public GameObject CablesFaseR, CablesFaseS;
        public GameObject IndicadorFaseS, IndicadorFaseT, IndicadorFaseR;
        public Material PCableMaterial, NCableMaterial;
        public float speed;
        public bool invertedField = false;
        private int inversionFactor = 1;
        private int speedFactor = 25;

        public void Flip()
        {
            if (this.speed > 0)
            {
                VectorFaseR.GetComponent<ArrowOscilator>().setDesfasaje(0f);
                VectorFaseS.GetComponent<ArrowOscilator>().setDesfasaje(120f);
                VectorFaseT.GetComponent<ArrowOscilator>().setDesfasaje(240f);
                IndicadorFaseR.GetComponentInChildren<Text>().text = "Fase R";
                IndicadorFaseS.GetComponentInChildren<Text>().text = "Fase S";
                ChangeMaterial(CablesFaseR, PCableMaterial);
                ChangeMaterial(CablesFaseS, NCableMaterial);
            }
            else if (this.speed.Equals(0))
            {
                VectorFaseS.SetActive(false);
                VectorFaseR.SetActive(false);
                VectorFaseT.SetActive(false);
            }
            else
            {
                VectorFaseR.GetComponent<ArrowOscilator>().setDesfasaje(0f);
                VectorFaseT.GetComponent<ArrowOscilator>().setDesfasaje(120f);
                VectorFaseS.GetComponent<ArrowOscilator>().setDesfasaje(240f);
                IndicadorFaseR.GetComponentInChildren<Text>().text = "Fase S";
                IndicadorFaseS.GetComponentInChildren<Text>().text = "Fase R";
                ChangeMaterial(CablesFaseR, NCableMaterial);
                ChangeMaterial(CablesFaseS, PCableMaterial);
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

        public void invertField()
        {
            invertedField = !invertedField;
            inversionFactor = -inversionFactor;
            speed = -speed;
            Flip();
            CentralObject.GetComponent<Spin>().speed *= -1;
        }

        public void setSpeed(float speed)
        {
            this.speed = inversionFactor * speed;
            setArrowsSpeed();
        }

        public void setArrowsSpeed()
        {
            VectorFaseR.GetComponent<ArrowOscilator>().timeFactor = speed/speedFactor;
            VectorFaseS.GetComponent<ArrowOscilator>().timeFactor = speed/speedFactor;
            VectorFaseT.GetComponent<ArrowOscilator>().timeFactor = speed/speedFactor;
        }
    }
}
