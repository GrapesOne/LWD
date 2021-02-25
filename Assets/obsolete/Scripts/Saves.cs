using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Saves : MonoBehaviour {

	public GameObject Anim;
	public GameObject Counter;
	public GameObject[] Sliders = new GameObject[3];
	GroundAnimator AnimatorSaver;
	Counters Count;

	/*void Start () {
		AnimatorSaver = Anim.GetComponent<GroundAnimator> ();
		Count = Counter.GetComponent<Counters> ();

		Count.AllCounters["blb"] = PlayerPrefs.GetInt ("BLB", 3);
		Count.AllCounters["BallSphere"] = PlayerPrefs.GetInt ("BallSphere", 0);
		Count.AllCounters ["MaxLvl"] = PlayerPrefs.GetInt ("MaxLvl", 0);
		Count.AllCounters["AllSecondSpent"] = PlayerPrefs.GetInt ("SecondsSpent", 0);
		Count.AllCounters["StartTime"] = PlayerPrefs.GetInt ("StartTime", 15);

		AnimatorSaver.NowSet = PlayerPrefs.GetInt ("Set", 0);
		AnimatorSaver.MinFactor = PlayerPrefs.GetInt ("Min", 20);
		AnimatorSaver.MaxFactor = PlayerPrefs.GetInt ("Max", 40);

		Sliders [0].GetComponent<Slider> ().value = (float) AnimatorSaver.MaxFactor / 100f;
		Sliders [1].GetComponent<Slider> ().value = (float) AnimatorSaver.MinFactor / 100f;
		Sliders [2].GetComponent<Slider> ().value = AnimatorSaver.NowSet;

		StartCoroutine (Saver ());
	}

	IEnumerator Saver () {
		yield return new WaitForSeconds (0.1f);

		PlayerPrefs.SetInt ("BLB", Count.AllCounters["blb"]);
		yield return new WaitForEndOfFrame();
		PlayerPrefs.SetInt ("BallSphere", Count.AllCounters["BallSphere"]);
		yield return new WaitForEndOfFrame();
		PlayerPrefs.SetInt ("MaxLvl", Count.AllCounters["MaxLvl"]);
		yield return new WaitForEndOfFrame();
		PlayerPrefs.SetInt ("SecondsSpent", Count.AllCounters["AllSecondSpent"]);
		yield return new WaitForEndOfFrame();
		PlayerPrefs.SetInt ("StartTime", Count.AllCounters["StartTime"]);
		yield return new WaitForEndOfFrame();
		PlayerPrefs.SetInt ("Set", AnimatorSaver.NowSet);
		yield return new WaitForEndOfFrame();
		PlayerPrefs.SetInt ("Min", AnimatorSaver.MinFactor);
		yield return new WaitForEndOfFrame();
		PlayerPrefs.SetInt ("Max", AnimatorSaver.MaxFactor);
		yield return new WaitForEndOfFrame();
		PlayerPrefs.Save ();

		yield return new WaitForSeconds (1f);
		StartCoroutine (Saver ());
		yield break;
	}*/
}
