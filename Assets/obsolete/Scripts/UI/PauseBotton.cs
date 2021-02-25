using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBotton : MonoBehaviour {
	private float tmpl = 0;
	public static PauseBotton Instance { private set; get; }
	public AudioSource Source; 
	void Awake()
	{
		Instance = this;
	}
	public void Pause()
	{
		AnimationController.ForcedUsingInterface();
		//Debug.Log("!!!!!!!!!!!!!!!!!!!");
		tmpl += Time.timeScale;
		Time.timeScale = tmpl - Time.timeScale;
		tmpl -= Time.timeScale;
		Source.Play();
	}
}
