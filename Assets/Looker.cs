using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looker : MonoBehaviour {
	public new Camera camera;
	private Vector3 fw;

	void Start () {
		// Defaults to ArCamera if none is specified.
		if (!camera) {
			camera = GameObject.Find ("ARCamera").GetComponent<Camera>();
		} // TODO: could add augmentation camera and other posibilities.
	}

	// Look at the camera.
	void Update () {
		if (camera) {
			transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
		}
	}
}
