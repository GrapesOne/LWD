using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointDisplayMove : MonoBehaviour {

	public GameObject InterfaceOb;
	float StartPosX;
	void Start()
	{
		StartPosX = transform.position.x;
	}
	void Update()
	{
		transform.position = new Vector3 (StartPosX + InterfaceOb.transform.position.x,
			InterfaceOb.transform.position.y, transform.position.z);
	}
}
