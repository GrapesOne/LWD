using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
[System.Serializable]
public class DatasTrajectory {
	[HideInInspector] public GameObject[] NewPoint = new GameObject[5];
	[Space]
	[HideInInspector] public float[] XPoint = new float[5];
	[HideInInspector] public float[] YPoint = new float[5];
	[Space]
	public int Steps = 5;
	public float Lenght = 1;
	public float XMult = 1;
	public float YMult = 1;
	[Space]
	[HideInInspector] public float OneStep = 0;
	[HideInInspector] public float QuanMultip = 1;
	[HideInInspector] public float FinalX;
	[HideInInspector] public float FinalY;
	[HideInInspector] public float XCreate, YCreate;
	[Space]
	public float StartXCreate;
	public float StartYCreate;

	
	
	public void TrajectoryCalculating(float Quan, GameObject point)
	{
		foreach (var t in NewPoint) Object.DestroyImmediate(t);
		NewPoint = new GameObject[Steps];
		for (var i = 0; i < NewPoint.Length; i++)
			NewPoint[i] = Object.Instantiate(point, new Vector3(StartXCreate, StartYCreate, -10),
				Quaternion.identity);
	
		XPoint = new float[Steps];
		YPoint = new float[Steps];
		
		QuanMultip = 1;
		for (var i = 0; i < Steps; i++)
			QuanMultip *= Quan;  //множитель в степени

		OneStep = 0;
		OneStep = ((Quan - 1) * Lenght) / (QuanMultip - 1);// формула первого элемента

		XPoint [0] = OneStep * XMult;    // первая точка х
		YPoint [Steps - 1] =OneStep * YMult;  // последняя точка у

		FinalX = XPoint [0];
		FinalY = YPoint [Steps - 1];
		XCreate = StartXCreate;
		YCreate = StartYCreate;

		

		for (var i = 1; i < Steps; i++)
		{

			XPoint [i] = XPoint [i - 1] * Quan;
			FinalX += XPoint[i];

			YPoint [Steps - 1 - i] = YPoint [Steps - i] * Quan;
			FinalY += YPoint [Steps - 1 - i];

			XCreate += XPoint [i-1];
			YCreate += YPoint [i-1];
			NewPoint [i-1].transform.position = new Vector3 (XCreate, YCreate, -10);
		}
		XCreate += XPoint [Steps - 1];
		YCreate += YPoint [Steps - 1];
		NewPoint [Steps - 1].transform.position = new Vector3 (XCreate, YCreate, -10);
	}
}
