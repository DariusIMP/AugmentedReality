using Dialogues;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSlide : MonoBehaviour
{

    public List<GameObject> elements;
    public string Title;
    

    public ConstructionSlide(string title, params GameObject[] elements)
    {
        this.elements = new List<GameObject>(collection: elements);
        this.Title = title;
    }

    public void Hide()
    {
        foreach (GameObject element in elements)
        {
            if (elements != null)
                element.SetActive(false);
        }
    }

    public void Show()
    {
        foreach (GameObject element in elements)
        {
            if (element != null)
                element.SetActive(true);
        }
    }

    public void Play()
    {
        GetComponent<DialogueManager>().Play();
    }

    public void Stop()
    {
        GetComponent<DialogueManager>().Stop();
    }

}
