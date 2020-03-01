using UnityEngine;

namespace Custom.Scripts
{
    public class DisplayMenu : MonoBehaviour
    {
        public GameObject menuDisplay = null;
        public GameObject controlDisplay = null;

        void Start()
        {
            menuDisplay.SetActive(false);
        }

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
            if (!menuDisplay.activeSelf)
                menuDisplay.SetActive(true);
        }

        public void HideControls()
        {
            controlDisplay.SetActive(false);
        }

        public void ShowControls()
        {
            if (controlDisplay.activeSelf) return;
            controlDisplay.SetActive(true);
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
}
