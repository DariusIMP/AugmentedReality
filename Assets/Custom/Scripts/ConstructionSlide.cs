using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSlide 
{

    private readonly ICollection<GameObject> elements;
    public string Title;
    

    public ConstructionSlide(string title, params GameObject[] elements)
    {
        this.elements = new HashSet<GameObject>(collection: elements);
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

}
