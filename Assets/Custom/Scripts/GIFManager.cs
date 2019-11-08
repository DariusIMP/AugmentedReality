using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GIFManager : MonoBehaviour
{

    public List<Sprite> Frames;
    public float FrameDuration;


    private void Start()
    {
        gameObject.GetComponent<Image>().sprite = Frames[0];
    }

    private void Update()
    {
        int frameIndex = (int)((Time.time / FrameDuration) % Frames.Count);
        gameObject.GetComponent<Image>().sprite = Frames[frameIndex];
    }

}
