using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowScaler : MonoBehaviour
{
	private static float MIN_SCALE = 10E-10f;
	
	// Length of the arrow.
	public float length = 1f;
	// Scale of the length of the arrow.
	public float scaleFactor = 0.1f;

	private Transform head;
	private Transform body;
	private Transform label;
	public string text = "P";

	void Start () {
		head = gameObject.transform.Find ("Head");
		body = gameObject.transform.Find ("Body");
		label = gameObject.transform.Find ("Label");
		label.Find("Text").GetComponent<Text> ().text = text;

		resize (length);
	}

	public void resize(float length) {
		float l = Math.Abs(length) * scaleFactor;
		gameObject.SetActive(l > MIN_SCALE);
		head.localPosition = new Vector3 (l, 0, 0);
		label.localPosition = new Vector3 (l + 2f, 0, 0);
		body.localScale = new Vector3 (-l, body.localScale.y, body.localScale.z);
			
	}

	public void rename(string text)
	{
		label.Find("Text").GetComponent<Text> ().text = text;
	}

	public void setScaleFactor(float factor)
	{
		scaleFactor = factor;
	}
}
