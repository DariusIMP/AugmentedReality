using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CameraSync : MonoBehaviour {
	public Camera arCamera;
	public Camera augmentationCamera;
	private bool sync = false;
	public float ScaleDistance = 1f;

	
	// Update is called once per frame
	void Update () {
		if (!sync) {
			augmentationCamera.projectionMatrix = arCamera.projectionMatrix;
		}
		setScaleDistance(ScaleDistance);
	}

	void setScaleDistance (float s) {
		transform.rotation = arCamera.transform.rotation;
		Vector3 sv = new Vector3(s, s, s);
		Vector3 p = arCamera.transform.position;
		p.Scale (sv);
		transform.position = p;
	}


}
