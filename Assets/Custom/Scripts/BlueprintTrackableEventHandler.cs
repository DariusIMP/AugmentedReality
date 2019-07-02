using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintTrackableEventHandler : DefaultTrackableEventHandler
{

    public GameObject slidesButtons;


    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        slidesButtons.SetActive(true);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        slidesButtons.SetActive(false);
    }
}
