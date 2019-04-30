using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogglePlayImage : MonoBehaviour {
	
	public Sprite playSprite;
	public Sprite pauseSprite;
	
	private bool playing = false;

	// Update is called once per frame
	public void togglePlay() {
		if (playing) {
			setPlayButtonAvailible ();
		} else {
			setPauseButtonAvailible ();
		}
	}

	public void setPauseButtonAvailible() {
		playing = true;
		GetComponent<Image> ().sprite = pauseSprite;
	}

	public void setPlayButtonAvailible() {
		playing = false;
		GetComponent<Image> ().sprite = playSprite;
	}
}
