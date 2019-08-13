using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts
{
    public class FadingText : MonoBehaviour
    {
        private FadingEffects _fadingEffects;
        
        public String textToShow;
        
        public float holdingTime, fadingTime;

        public void Start()
        {
            _fadingEffects = gameObject.AddComponent<FadingEffects>();
        }

        public void ShowFadingText()
        {
            Text titulo = gameObject.GetComponent<Text>();
            titulo.text = textToShow;
            StartCoroutine(_fadingEffects.ShowAndHideTextFading(fadingTime, holdingTime, titulo));
        }
    }
}
