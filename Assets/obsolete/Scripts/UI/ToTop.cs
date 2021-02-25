using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTop : MonoBehaviour {
	protected GameObject Interface, PointsDisplay, PlayerOb;
	protected Generation GenerationManager;
	protected CameraManager Cam;
	protected Dictionary<string, Vector3>  PositionOfobjects = new Dictionary<string, Vector3>
	{
		{"PlayerOb", new Vector3 (0f, 6.5f, 0f)},
		{"Interface", new Vector3 (0f, 0f, -10f)},
		{"PointsDisplay", new Vector3 (-9.5f, -1f, -10f)},
		{"Cam", new Vector3 (-9.5f, -1f, -40f)}
	};
	protected float seconds;


	void Start()
	{
		PositionOfObjects ();
		GenerationManager = GameObject.Find ("Generator").GetComponent<Generation> ();
		Cam =  GameObject.Find ("Main Camera").GetComponent<CameraManager> ();
		PlayerOb = GameObject.Find ("Player");
		Interface = GameObject.Find ("Interface");
		PointsDisplay = GameObject.Find ("PointsDisplay");
	}

	protected virtual void MoveCameraAndState () {
		
	}
	protected virtual void PositionOfObjects () {

	}

	void OnMouseDown()
	{
		StartCoroutine (Restart ());
	}
	protected virtual IEnumerator Restart()
	{
		PositionOfObjects ();
		PointsDisplay.GetComponent<PointDisplayMove> ().enabled = false;
		Cam.nowTarget = 5;
		GenerationManager.GenDone = false;
		StartCoroutine(GenerationManager.AllStackRemove ());
		yield return new WaitForSeconds (seconds);
		Cam.nowTarget = 5;
		PlayerOb.transform.position = PositionOfobjects["PlayerOb"];
		Interface.GetComponent<InterfaceMove> ().LowestPoint = 0;
		Interface.transform.position = PositionOfobjects["Interface"];
		PointsDisplay.transform.position = PositionOfobjects["PointsDisplay"];
		Cam.gameObject.transform.position = PositionOfobjects["Cam"];
		GenerationManager.ToStart ();
		MoveCameraAndState ();
		PointsDisplay.GetComponent<PointDisplayMove> ().enabled = true;
		yield break;
	}

}
