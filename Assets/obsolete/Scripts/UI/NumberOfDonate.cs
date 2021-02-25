using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOfDonate : MonoBehaviour {

	string nameOf;
	Counters count;


	void Start () {
		count = GameObject.FindGameObjectWithTag ("CountManager").GetComponent<Counters> ();
		nameOf = gameObject.name;
		nameOf = nameOf.Substring (0, nameOf.IndexOf ('T'));
	}
	

	void Update () {
			//GetComponent<Text> ().text = count.AllCounters[nameOf].ToString();
	}
}
