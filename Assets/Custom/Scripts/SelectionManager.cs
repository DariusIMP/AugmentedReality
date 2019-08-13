using UnityEngine;

namespace Custom.Scripts
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private string selectableTag = "Selectable";
        [SerializeField] private Material highlightMaterial;
        [SerializeField] private Material defaultMaterial;
        private GameObject selectedObject;
        private Transform _selection;
        public Camera camera;
    
        private void Update()
        {
            if (_selection != null)
            {
                var selectionRenderer = _selection.GetComponent<Renderer>();
                selectionRenderer.material = defaultMaterial;
                _selection = null;
                selectedObject.GetComponent<DisplayMenu>().HideMenu();
            }

            var cam = camera.transform;
            var ray = new Ray(cam.position, cam.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
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
        }
    }
}
