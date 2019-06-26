using System;
using UnityEngine;

namespace Custom.Scripts.CampoRotanteScripts
{
    public class CampoRotanteArrowController : MonoBehaviour
    {

        public float scaleFactor = 0.1f;
        public float length = 1f;
        protected Transform head;
        protected Transform body;
        protected bool inverted = true;

        // Use this for initialization
        public void Start()
        {
            head = gameObject.transform.Find("Head");
            body = gameObject.transform.Find("Body");
            inverted = length < 0;
            resize(length);
        }
        
        public void resize(float length)
        {
            float l = Math.Abs(length) * scaleFactor;
            gameObject.SetActive(true);
            head.localPosition = new Vector3(l, 0, 0);
            body.localScale = new Vector3(-l, body.localScale.y, body.localScale.z);
        }

        public Vector3 GetVectorScale()
        {
            return body.localScale;
        }

        public Vector3 GetHeadPosition()
        {
            return head.position;
        }
    }
}
