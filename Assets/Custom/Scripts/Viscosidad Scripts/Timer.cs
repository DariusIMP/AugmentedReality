using UnityEngine;
using UnityEngine.UI;

namespace Viscosidad_Scripts
{
    public class Timer : MonoBehaviour
    {
        public GameObject timer;
    
        Text text;
        float theTime;
        public float speed = 1;
        bool playing;

        // Use this for initialization
        void Start ()
        {
            text = timer.GetComponent<Text>();
        }
 
        // Update is called once per frame
        void Update () {
            if (playing)
            {
                theTime += Time.deltaTime * speed;
                string hours = Mathf.Floor((theTime % 216000) / 3600).ToString("00");
                string minutes = Mathf.Floor((theTime % 3600) / 60).ToString("00");
                string seconds = (theTime % 60).ToString("00");
                string milli = Mathf.Floor((theTime * 1000) % 1000).ToString("000");﻿
                text.text = hours + ":" + minutes + ":" + seconds + ":" + milli;
            }
        }

        public void ClickPlay ()
        {
            playing = true;
        }

        public void ClickStop()
        {
            playing = false;  
        }

        public void Restart()
        {
            theTime = 0;
            text.text = "00:00:00:00";
            playing = false;
        }
    }
}