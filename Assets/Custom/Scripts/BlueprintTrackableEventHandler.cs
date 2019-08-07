using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintTrackableEventHandler : DefaultTrackableEventHandler
{

    public List<GameObject> UIObjects;


    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        foreach (GameObject obj in UIObjects)
        {
            obj.SetActive(true);
        }
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        foreach (GameObject obj in UIObjects)
        {
            obj.SetActive(false);
        }
    }
}
