using System;
using UnityEngine;

public abstract class PeralteView : MonoBehaviour
{
	// Prefab de diagrama de cuerpo libre.
	public GameObject FBDObject;
	public abstract void setActive(bool active);
	public abstract void Start();

	protected GameObject instantiateFBD(string name) {
		GameObject FBD = Instantiate(FBDObject, new Vector3 (0f, 0f, 0f), Quaternion.identity);
		FBD.SetActive(false);
		FBD.name = name;
		FBD.transform.SetParent(this.transform, false);
		return FBD;
	}
}
