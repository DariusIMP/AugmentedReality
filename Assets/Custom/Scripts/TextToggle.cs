using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * TextToggle
 * Script for changing the text of a toggle button; if it's activated it should be written the toggledText
 * and otherwise the untoggledText should appear.
 *
 * This script should be on the gameobject in question, which should have a Text component as a children.
 */
public class TextToggle : MonoBehaviour
{
    public string untoggledText;

    public string toggledText;

    private bool _isToggled = false;

    public void ToggleText()
    {
        if (!_isToggled)
        {
            gameObject.GetComponentInChildren<Text>().text = toggledText;
            _isToggled = true;
        }
        else
        {
            gameObject.GetComponentInChildren<Text>().text = untoggledText;
            _isToggled = false;
        }
    }
    
}
