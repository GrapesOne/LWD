using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenPosition : MonoBehaviour {
	[SerializeField]float XNow = 1;
	[SerializeField]float XScale = 1;
	void Update () {
		XScale = (float) Screen.width / Screen.height;
		transform.localPosition = new Vector3 (XNow * XScale, transform.localPosition.y, transform.localPosition.z);
		//Destroy (this, 0.2f);
	}

}
