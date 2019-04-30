using UnityEngine;
using System.Collections;
using Vuforia;

public class CameraFocusController : MonoBehaviour {

    private bool mVuforiaStarted = false;

    void Start () 
    {
        Debug.Log("CAMERAFOCUSCONTROLLER: Start called");

        VuforiaARController vuforia = VuforiaARController.Instance;

        if (vuforia != null)
            vuforia.RegisterVuforiaStartedCallback(StartAfterVuforia);
    }

    private void StartAfterVuforia()
    {
        mVuforiaStarted = true;
        SetAutofocus();
    }

    void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            // App resumed
            if (mVuforiaStarted)
            {
                // App resumed and vuforia already started
                // but lets start it again...
                SetAutofocus(); // This is done because some android devices lose the auto focus after resume
                // this was a bug in vuforia 4 and 5. I haven't checked 6, but the code is harmless anyway
            }
        }
    }

    private void SetAutofocus()
    {
        if (CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO))
        {
            Debug.Log("CAMERAFOCUSCONTROLLER: Autofocus set");
        }
        else
        {
            // never actually seen a device that doesn't support this, but just in case
            Debug.Log("CAMERAFOCUSCONTROLLER: this device doesn't support auto focus");
            CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
        }
    }

    public void TriggerFocus()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);

        //para que no se desconfigure para los dispositivos que tienen autofocus:
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);

		Exposition ();
    }

	public void Exposition(){
		Debug.Log ("CAMERAFOCUSCONTROLLER Exposition started");
		foreach (var cameraField in CameraDevice.Instance.GetCameraFields ()) {
			Debug.Log("CAMERAFOCUSCONTROLLER Exposition:" + cameraField.Key + "-" + cameraField.Type);
		} 

	}

}
