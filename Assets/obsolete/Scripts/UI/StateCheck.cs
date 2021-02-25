using System.Collections;
using System.Collections.Generic;
using System;
using System.Security;
using UnityEngine;
using UnityEngine.UI;

public class StateCheck : INeedCam {
	
	[SerializeField] int State = 0;
	[SerializeField] string[] ComponentNames = new string[0];
	[SerializeField] bool OnThisState = true;


	protected override void WithStartAnother()
	{
		StartCoroutine (Checker (State));
	}

	IEnumerator Checker(int State)
	{
		int i;
		for (i = 0; i + 2 < ComponentNames.Length; i += 2) { 
			((Behaviour) GetComponent (ComponentNames [i])).enabled = Check (State);
			((Behaviour) GetComponent (ComponentNames [i+1])).enabled = Check (State);
		}
		for (; i < ComponentNames.Length; i ++)
			((Behaviour) GetComponent (ComponentNames [i])).enabled = Check (State);
		yield return new WaitForSeconds (0.2f);
		StartCoroutine (Checker (State));
		yield break;
	}
	bool Check(int State)
	{
		return Cam.nowTarget == State ? OnThisState : !OnThisState;
	}



}
