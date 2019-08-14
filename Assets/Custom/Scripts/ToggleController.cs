using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts
{
    public class ToggleController : MonoBehaviour
    {
    
        private Toggle toggle;
 
        private void Start()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }
 
        private void OnToggleValueChanged(bool isOn)
        {
            ColorBlock cb = toggle.colors;
            if (isOn)
            {
                cb.normalColor = Color.red;
                cb.highlightedColor = Color.red;
            }
            else
            {
                cb.normalColor = Color.white;
                cb.highlightedColor = Color.white;
            }
            toggle.colors = cb;
        }
    }
}
