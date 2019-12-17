using System;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts.Film.UI_Scripts
{
    public class ToggleAudio : MonoBehaviour
    {
        public GameObject Film;
        private Film _Film;
        public Sprite audioOnSprite;
        public Sprite audioOffSprite;
	
        private bool audioSet = true;

        private void Start()
        {
            _Film = Film.GetComponent<Film>();
        }

        // Update is called once per frame
        public void toggleAudio() {
            if (audioSet) {
                setAudioOff ();
            } else {
                setAudioOn ();
            }
        }

        public void setAudioOn() {
            audioSet = true;
            _Film.GetCuadroActual().TurnAudioOn();
            GetComponent<Image> ().sprite = audioOffSprite;
        }

        public void setAudioOff() {
            audioSet = false;
            _Film.GetCuadroActual().TurnAudioOff();
            GetComponent<Image> ().sprite = audioOnSprite;
        }
    }
}