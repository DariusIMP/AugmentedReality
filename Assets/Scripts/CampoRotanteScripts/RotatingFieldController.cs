﻿using UnityEngine;

namespace Campo_Rotante_Scripts
{
    public class RotatingFieldController : MonoBehaviour
    {
        public GameObject VectorFaseS, VectorFaseT, VectorFaseR;
        public GameObject CentralObject;
        public GameObject CablesFaseR, CablesFaseS;
        public Material PCableMaterial, NCableMaterial;
        public float speed = 0;
        public bool invertedField = false;

        public void Flip()
        {
            if (this.speed < 0)
            {
                VectorFaseR.GetComponent<ArrowOscilator>().setDesfasaje(0f);
                VectorFaseS.GetComponent<ArrowOscilator>().setDesfasaje(120f);
                VectorFaseT.GetComponent<ArrowOscilator>().setDesfasaje(240f);

                ChangeMaterial(CablesFaseR, PCableMaterial);
                ChangeMaterial(CablesFaseS, NCableMaterial);
            }
            else
            {
                VectorFaseR.GetComponent<ArrowOscilator>().setDesfasaje(0f);
                VectorFaseT.GetComponent<ArrowOscilator>().setDesfasaje(120f);
                VectorFaseS.GetComponent<ArrowOscilator>().setDesfasaje(240f);

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
            setSpeed(speed);
            Flip();
        }

        public void setSpeed(float speed)
        {
            if (!invertedField)
            {
                this.speed = speed;
            }
            else
            {
                this.speed = -speed;
            }
        }
    }
}
