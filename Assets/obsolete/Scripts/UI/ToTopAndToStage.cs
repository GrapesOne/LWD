using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTopAndToStage :ToTop {
	[SerializeField][Range(0,5)] int State = 0;
	[SerializeField][Range(0f,1f)] float Seconds = 0.4f;
	protected override void MoveCameraAndState ()
	{
		Cam.nowTarget = State;
	}
	protected override IEnumerator Restart()
	{
		ButtonOff.Instance.ButtonOffs();
		PointsDisplay.GetComponent<PointDisplayMove>().enabled = false;
		GenerationManager.GenDone = false;
		StartCoroutine(GenerationManager.AllStackRemove());
		//yield return new WaitForSeconds(seconds);
		PlayerOb.transform.position = PositionOfobjects["PlayerOb"];
		Interface.GetComponent<InterfaceMove>().LowestPoint = 0;
		Interface.transform.position = PositionOfobjects["Interface"];
		PointsDisplay.transform.position = PositionOfobjects["PointsDisplay"];
		Cam.gameObject.transform.position = PositionOfobjects["Cam"];
		//yield return new WaitForSeconds(0.05f);
		GenerationManager.ToStart();
		//yield return new WaitForSeconds(0.05f);
		MoveCameraAndState();
		PointsDisplay.GetComponent<PointDisplayMove>().enabled = true;
		yield break;
	}
}
