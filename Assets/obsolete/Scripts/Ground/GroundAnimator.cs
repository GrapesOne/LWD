using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAnimator : MonoBehaviour {
	public GroundColorSet[] Sets;
	[Range(0, 6)] public int NowSet;
	[Range(1, 100)] public int MaxFactor=50, MinFactor =20;
	public static GroundAnimator Instance { private set; get; }
	public delegate void MethodContainer();
	public static event MethodContainer Setter;
	public static event MethodContainer FactorChanged;

	void Awake()
	{
		Instance = this;
	}
	public void MaxFactorChange(float NewFactor)
	{
		MaxFactor = (int)(100*NewFactor);
		FactorChanged?.Invoke();
	}
	public void MinFactorChange(float NewFactor)
	{
		MinFactor = (int)(100*NewFactor);
		FactorChanged?.Invoke();
	}

	public void SetChange(float NewSet)
	{
		Setter?.Invoke ();
		NowSet = (int)NewSet;
	}

	void Update()
	{
		if (Time.frameCount % 500 == 0) FactorChanged?.Invoke();
	}
}
