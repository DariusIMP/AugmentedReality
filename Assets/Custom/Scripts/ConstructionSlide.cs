using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSlide 
{

    private readonly List<GameObject> elements;
    public string Name;
    

    public ConstructionSlide(string name, params GameObject[] elements)
    {
        this.elements = new List<GameObject>(collection: elements);
        this.Name = name;
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

}
