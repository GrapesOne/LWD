using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUIClick : MonoBehaviour {
	AudioSource Audio;
	void Awake ()
	{
		Audio = gameObject.GetComponent<AudioSource> ();
	}
	void OnMouseDown()
	{
		Audio.Play ();
		ButtonOff.Instance.ButtonOn();
	}
}
