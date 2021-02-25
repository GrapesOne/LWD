using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INeedCam : UIBehaviour {

	[System.NonSerialized] public CameraManager Cam;
	public static CameraManager _Cam;

	protected override void WithStart()
	{
		FoundCam ();
		WithStartAnother ();
	}
	protected virtual void WithStartAnother(){}

	protected void StateToCamera(int State)
	{ Cam.nowTarget = State;}

	protected void FoundCam()
	{
		Cam =  GameObject.Find ("Main Camera").GetComponent<CameraManager> ();
	}
	protected static void _FoundCam()
	{
		_Cam =  GameObject.Find ("Main Camera").GetComponent<CameraManager> ();
	}
	protected CameraManager ReturnCam()
	{
		return  GameObject.Find ("Main Camera").GetComponent<CameraManager> ();
	}

}
