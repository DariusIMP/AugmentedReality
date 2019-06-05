using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

	public Material[] material;
	Renderer m_Renderer;

	// Use this for initialization
	void Start () {
		//Fetch the Renderer from the GameObject
		m_Renderer = GetComponent<Renderer> ();
		m_Renderer.enabled = true;
		m_Renderer.sharedMaterial = material[0];
	}

	public void ChangeToMaterial(uint i)
	{
		m_Renderer.sharedMaterial = material[i];
	}
}
