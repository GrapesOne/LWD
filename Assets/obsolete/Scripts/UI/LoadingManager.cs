using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour {
	public string[] texts;
	public Text txt;

	IEnumerator Start()
	{
		yield return new WaitForEndOfFrame ();
		if (Generation.GenerationStage != 3) {
			if (Generation.GenerationStage != 0)
				txt.text = texts [Generation.GenerationStage - 1];
			StartCoroutine (Start ());
		} else {
			txt.text = texts [Generation.GenerationStage - 1];
			yield return new WaitForSeconds (0.2f);
			this.enabled = false;
		}
		yield break;
	}
}


