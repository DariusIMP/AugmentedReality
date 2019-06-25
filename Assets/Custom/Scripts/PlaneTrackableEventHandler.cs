using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTrackableEventHandler : DefaultTrackableEventHandler
{

    public GameObject slideButtons;


    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
        slideButtons.SetActive(true);
    }

    protected override void OnTrackingLost()
    {
        base.OnTrackingLost();
        slideButtons.SetActive(false);
    }
}
