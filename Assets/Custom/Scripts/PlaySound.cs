using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour {

	public AudioClip soundtrack;
	private AudioSource source;

	void Awake () {
		source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		source.PlayOneShot (soundtrack);
	}
}
	