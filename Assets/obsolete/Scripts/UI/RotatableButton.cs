using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableButton : MonoBehaviour {
	public float Angle=12f, Speed=35f;
	float RotateAngle;
	float plusSpeed ;
	int revert = 1;

	void Start () {
		
		RotateAngle = -Angle;
		transform.Rotate (0f, 0f, RotateAngle);
	}

	void Update()
	{

		if (RotateAngle < Angle) {
			plusSpeed = Time.deltaTime * Speed ;
			transform.Rotate (0f, 0f, plusSpeed * revert);
			RotateAngle += plusSpeed;
		} else {
			RotateAngle *= -1;
			revert *= -1;
		}
	}

}
