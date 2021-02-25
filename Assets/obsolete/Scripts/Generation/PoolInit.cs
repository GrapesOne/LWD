using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolInit : MonoBehaviour {

	void Awake () {
		foreach (Transform t in transform) Destroy(t.gameObject);
		PoolManager.init (transform);
	}

}
