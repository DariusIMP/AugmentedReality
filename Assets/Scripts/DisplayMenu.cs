using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMenu : MonoBehaviour
{
    public GameObject menuDisplay = null;

    void Start() { }

    public void SetMenuDisplay(GameObject menu)
    {
        menuDisplay.SetActive(false);
        menuDisplay = menu;
    }

    public void HideMenu()
    {
        menuDisplay.SetActive(false);
    }

    public void ShowMenu()
    {
        menuDisplay.SetActive(true);
    }
    
	public void Action() {
	    if (!menuDisplay.activeInHierarchy)
        {
            menuDisplay.SetActive(true);
        } else
        {
            menuDisplay.SetActive(false);
        }
    }

}
