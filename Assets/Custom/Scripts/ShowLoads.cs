using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLoads : MonoBehaviour
{

    static string ROOF_TAG = "Roof";
    static string WALL_TAG = "Wall";
    static float OPAQUE_ALPHA = 1.0f;
    static float TRANSPARENT_ALPHA = 0.3f;

    public GameObject loads;
    public GameObject building;


    // Start is called before the first frame update
    void Start()
    {
        this.loads.SetActive(false);
    }

    public void ToggleLoads()
    {

        // Toggle is valid only if virtual building is visible
        if (true /* TODO: Check if virtual building is on */)
        {
            bool show = !this.loads.activeSelf;
            this.loads.SetActive(show);
            ToggleBuilding(!show);
        }
    }

    private void ToggleBuilding(bool opaque)
    {
        foreach (Renderer childRenderer in this.building.GetComponentsInChildren<Renderer>(true))
        {
            if (childRenderer.tag.Equals(WALL_TAG))
            {
                Color color = childRenderer.material.color;
                color.a = opaque ? OPAQUE_ALPHA : TRANSPARENT_ALPHA;
                childRenderer.material.color = color;
            } else if (childRenderer.tag.Equals(ROOF_TAG))
            {
                childRenderer.gameObject.SetActive(opaque);
            }
        }
    }


}
