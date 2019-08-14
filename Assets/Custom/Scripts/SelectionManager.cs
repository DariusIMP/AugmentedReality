using System;
using UnityEngine;
using UnityEngine.UI;

namespace Custom.Scripts
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private string selectableTag = "Selectable";
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material defaultMaterial;
        private GameObject selectedObject;
        private Transform _selection;
        private bool selectionLocked;
        private bool displayInfoMode;
        private FadingText _fadingText;
        public Camera camera;
        public GameObject aimingDot;
        public GameObject instructions;

        private void Start()
        {
            _fadingText = instructions.AddComponent<FadingText>();
            _fadingText.textToShow = "Presione la pantalla para fijar un elemento";
            _fadingText.holdingTime = 0.5f;
            _fadingText.fadingTime = 1f;
        }

        private void Update()
        {
            if (displayInfoMode)
            {
                if (!selectionLocked)
                {
                    if (_selection != null)
                    {
                        DeselectObject();
                    }
                    var cam = camera.transform;
                    var ray = new Ray(cam.position, cam.forward);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        SelectObject(hit);
                    }
                }
            }
        }

        private void DeselectObject()
        {
            var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            _selection = null;
            selectedObject.GetComponent<DisplayMenu>().HideMenu();
        }

        private void SelectObject(RaycastHit hit)
        {
            var selection = hit.transform;
            if (selection.CompareTag(selectableTag))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                    selectedObject = hit.collider.gameObject;
                    selectedObject.GetComponent<DisplayMenu>().ShowMenu();
                }
    
                _selection = selection;
            }
        }
        public void InfoDisplayModeToggle()
        {
            if (displayInfoMode)
            {
                if (_selection != null)
                {
                    DeselectObject();
                }
                aimingDot.SetActive(false);
                displayInfoMode = false;
            }
            else
            {
                _fadingText.ShowFadingText();
                displayInfoMode = true;
                aimingDot.SetActive(true);
            }
        }

        public void SelectionLockToggle()
        {
            Debug.Log("Selection Lock Toggle");
            if (selectionLocked)
            {
                selectionLocked = false;
                aimingDot.SetActive(true);
            }
            else
            {
                if (_selection != null)
                {
                    selectionLocked = true;
                    aimingDot.SetActive(false);
                }
            }
        }
    }
}
