using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Trajectory : MonoBehaviour {

	public GameObject Point;
	public DatasTrajectory[] Datas = new DatasTrajectory[3];
	public float Quan = 1.15f;
	
	[Button]
	void Calculate() {
		foreach (var t in Datas) t.TrajectoryCalculating(Quan, Point);
	}
	void Start()
	{
		
		
		foreach (var t in Datas) t.TrajectoryCalculating(Quan, Point);
		foreach (var t in Datas) foreach (var o in t.NewPoint) DestroyImmediate(o);
	}

}
