using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLaunch : INeedCam {
	
	protected override void WithStartAnother(){
		StartCoroutine (GenerationCheck ());
	}
	IEnumerator GenerationCheck(){
		yield return new WaitForSeconds (0.1f);
		if (Generation.GenerationStage == 3) {
			yield return new WaitForSeconds (0.1f);
			StateToCamera (1);
			this.enabled = false;
		} else
			StartCoroutine (GenerationCheck ());
		yield break;
	}
}
