using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideBarManager : MonoBehaviour
{
    
    public void ToggleOnly(DisplayMenu toggleMenu)
    {
        for (int i = 0; i < transform.childCount; ++i) {
            DisplayMenu menu = transform.GetChild(i).GetComponent<DisplayMenu>();
            if (menu != null && toggleMenu != menu)
                menu.HideMenu();
        }
        toggleMenu.Action();
    }

    public void CloseAll()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            DisplayMenu menu = transform.GetChild(i).GetComponent<DisplayMenu>();
            if (menu != null)
                menu.HideMenu();
        }
    }

}
