using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparency : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}

	public void Adjust_Transparency(float transparency)
	{
		GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, transparency);
	}

}
