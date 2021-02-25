using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Trajectory : MonoBehaviour {

	public GameObject Point;
	public DatasTrajectory[] Datas = new DatasTrajectory[3];
	public float Quan = 1.15f;
	private float simulationMultiplier;
	[Button]
	void Calculate() {
		foreach (var t in Datas) t.TrajectoryCalculating(Quan, Point);
	}
	void Start()
	{
		Application.targetFrameRate = Screen.currentResolution.refreshRate;
		simulationMultiplier = Screen.currentResolution.refreshRate / 120f;
		
		
		foreach (var t in Datas) t.TrajectoryCalculating(Quan, Point);
		foreach (var t in Datas) foreach (var o in t.NewPoint) DestroyImmediate(o);
	}

}
