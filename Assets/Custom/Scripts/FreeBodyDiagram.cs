using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeBodyDiagram : MonoBehaviour {

	public GameObject arrow;
	public static int count = 0;

	void Start () { 
	}

	// Creates an arrow in the xy plane with an angle in degrees from the x axis.
	public void addArrow (float angle, float length, string text) {
		addArrow (0, 0, angle, length, text);
	}

	// Creates an arrow specifying the euler angles.
	public void addArrow (float roll, float pitch, float yaw, float length, string text) {
		GameObject new_arrow = Instantiate (arrow, new Vector3 (0, 0, 0), Quaternion.identity);
		new_arrow.name = "new_arrow " + (count++);
		new_arrow.transform.localEulerAngles = new Vector3 (roll, pitch, yaw);
		new_arrow.transform.SetParent(this.transform, false);
		ArrowScaler new_arrow_scaler = new_arrow.GetComponent<ArrowScaler>();
		// Set the length. The arrow will be resized when "Start" is called.
		new_arrow_scaler.length = length;
		new_arrow_scaler.text = text;
		// TODO: this should not be necessary. Check
		new_arrow.SetActive(true);
	}

	void Update () {
		
	}
}
