using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class Generation : MonoBehaviour {
	public GameObject Counter, GPool;
	[Space]
	[SerializeField] GameObject[] Obs = new GameObject[5];
	[Space]
	[SerializeField] int MaxRnd = 500;
	[SerializeField] int MaxGround = 350;
	[SerializeField] int MaxSphere = 100;
	[SerializeField] int MaxEnemy = 60;
	[SerializeField] int MaxClock = 20;
	[SerializeField] int MaxBlb = 5;
	int[,] gm = new int[9,1500];
	GameObject[,] ObsOnLvl = new GameObject[9,1500];
	int cl, rnd, lastClock, lastCrystal, lastEnemy;
	float xpos, ypos, size, startxpos, startypos;
	public static int GenerationStage;
	float timer; 
	[Space]
	public int LvlSide;
	public int StepOfGeneration;
	public int StartOfStep;
	public int StepToDeleting;
	public bool GenDone ;




	IEnumerator Start () {
		yield return new WaitForEndOfFrame ();
		size = 1;
		xpos = -4 ;
		ypos = 2 ;
		LvlSide += 5;

		FirstLRWallCreate ();
		startxpos = xpos;
		startypos = ypos;

		GPool = GameObject.FindGameObjectWithTag ("Pool");

		yield return new WaitForEndOfFrame ();
		GenDone = true;
		yield break;
	}

	void Update()
	{
		if (Time.frameCount % 25 == 1) {
			if (GenDone) {
				if ((Counters.AllCounters["NowLvl"] * 5) >= ((LvlSide * (StepOfGeneration + 1)) - (StartOfStep * 5 ))) {
					GenDone = false;
					StepOfGeneration++;
					StartCoroutine (StackGeneration (StepOfGeneration));
					if ((LvlSide * (StepOfGeneration - StepToDeleting)) > 0)
						StartCoroutine (StackPooling (StepOfGeneration));
				}
			}
		}
	}

	IEnumerator StackGeneration(int Step)
	{
		yield return new WaitForSeconds (0.0001f);
		GenerationStage = 1;
		ArrayCreating (Step);
		yield return new WaitForSeconds (0.1f);
		GenerationStage = 2;
		StartCoroutine(ArrayArrangement (Step));
		yield return new WaitForSeconds (0.5f);
		GenerationStage = 3;
		GenDone = true;
		yield break;
	}

	IEnumerator StackPooling (int Step)
	{
		yield return new WaitForSeconds (0.01f);
		for (int i = (LvlSide * (Step - StepToDeleting)); i < (LvlSide * (Step - StepToDeleting + 1 )); i++) {
			if (i > 0) {
				yield return new WaitForSeconds (0.00f);
				for (int j = 0; j < gm.GetLength (0); j++) { 
					PoolManager.putGameObjectToPool (ObsOnLvl [j, i]);
					gm [j, i] = 0;
				}
			}
		}
		yield break;
	}

	IEnumerator StackDeleting (int Step)
	{
		yield return new WaitForSeconds (0.0001f);
		for (int i = (LvlSide * (Step - StepToDeleting )); i < (LvlSide * (Step - StepToDeleting + 1 )); i++) {
			yield return new WaitForSeconds (0.00f);
			if (i > 0) {
				for (int j = 0; j < gm.GetLength (0); j++) { 
					Destroy (ObsOnLvl [j, i]);
					gm [j, i] = 0;
				}
			}
		}
		yield break;
	}










	void ArrayCreating(int Step)
	{
		for (int i = (LvlSide*Step); i < (LvlSide*(Step+1)); i++) {
			lastClock--;
			lastCrystal--;
			lastEnemy--;
			for (int j = 0; j < gm.GetLength (0); j++) {
				rnd = Random.Range (0, MaxRnd);
				if (j == 0 || j == gm.GetLength (0) - 1) {
					gm [j, i] = 1;
				} else {
					ArrayFilling (i, j);
					ArrayFirstFix (i, j);
					RemovalOfUnnecessaryElements (i, j);
				}
			}
			ArrayFixing (i);
		}
	}











	IEnumerator ArrayArrangement(int Step)
	{
		yield return new WaitForSeconds (0.00f);
		for (int i = (LvlSide*Step); i < (LvlSide*(Step+1)); i++) {
			
			for (int j = 0; j < gm.GetLength (0); j++) {
				if (Obs [gm [j, i]] != null) {
					yield return new WaitForSeconds (0.00f);
					ObsOnLvl [j, i] = new GameObject ();
					ObsOnLvl [j, i] = PoolManager.getGameObjectFromPool(Obs [gm [j, i]],new Vector3(xpos, ypos, 0));
					ObsOnLvl [j, i].transform.position = new Vector3(xpos, ypos, 0);
				}
				xpos += size;
			}
			xpos = -4 * size;
			ypos -= size;
		}
		yield break;
	}

	void ArrayFilling(int i, int j)
	{
		if (gm [j, i] == 0) {
			if (rnd < MaxGround) {
				gm [j, i] = 1;
				cl++;
			}
			if (rnd < MaxSphere)
				gm [j, i] = 2;
			
			if (rnd < MaxEnemy && lastEnemy < 0) {
				gm [j, i] = 3;
				lastEnemy = 5;
				gm [j, i + 1] = 1;
				cl++;
			}
			if (rnd < MaxClock && lastClock < 5) {
				gm [j, i] = 4;
				lastClock = 10;
			}
			if (rnd < MaxBlb && lastCrystal < 0) {
				gm [j, i] = 5;
				lastCrystal = 50;
			}
		}
	}











	void ArrayFirstFix(int i, int j){
		if (i > 0 && gm [j, (i - 1)] != 0) {
			if (i > 1 && gm [j, (i - 2)] != 0) {
				if ((j > 1 && gm [(j - 1), i] != 0)
					|| (j < gm.GetLength (0) - 2 && gm [(j + 1), i] != 0)) {
					gm [j, i] = 1;
					cl++;
				}
			}
		}
		if (i > 0 && gm [j, (i - 1)] == 1) {
			if ((j > 1 && gm [(j - 1), i] == 1)
				|| (j < gm.GetLength (0) - 2 && gm [(j + 1), i] == 1)) {
				cl++;
			}
		}
	}











	void ArrayFixing(int i)
	{ int j , iud =  0;
		
		for (j = 1; j < gm.GetLength (0) - 1; j++)
		{
			if (gm[j, i] == 0) continue;
			iud++;
			ud(i, ref iud, ref j);
		}
	}

	void ud(int i , ref int iud, ref int j)
	{ 
		int iiud = 0, plat = 0;
		j++;
		for (; j < gm.GetLength (0)-1; j++){
			if (i + iud >= 1500 || i - iud <= 0)  break;
			for (var n=0; n<=iud; n++) {
				if (gm [j, i + n] == 1) {
					iiud = i + n;
					plat++;
				}
				if (gm[j, i - n] != 1) continue;
				iiud = i + n;
				plat++;
			}
			iud = iud < Mathf.Abs (iiud - i) + 1 ? Mathf.Abs (iiud - i) + 1 : iud;
			if (plat <= gm.GetLength(0) - j + 2) continue;
			for (var n = 0; n != iud; n++) {
				gm [j - 1, i + n] = 0; 
				gm [j - 1, i - n] = 0;
				plat = 0;
			}
			break;

		}
	}
	
	
	void RemovalOfUnnecessaryElements(int i, int j)
	{
		while (cl > 5) {
			gm [Random.Range (1, gm.GetLength (0)-1), i] = 0;
			gm [Random.Range (1, gm.GetLength (0)-1), i] = 0;
			gm [Random.Range (1, gm.GetLength (0)-1), i] = 0;
			cl -= 3;
		}
	}

	void FirstLRWallCreate()
	{
		for (var i = 0; i < 5; i++) {
			gm [0, i] = 1;
			gm [gm.GetLength (0) - 1, i] = 1;

			ObsOnLvl [0, i] = PoolManager.getGameObjectFromPool(Obs [gm [0, i]],new Vector3(xpos, ypos, 0));
			ObsOnLvl [0, i].transform.position = new Vector3 (xpos, ypos, 0);

			ObsOnLvl [gm.GetLength (0) - 1, i] = PoolManager.getGameObjectFromPool(
				Obs [gm [gm.GetLength (0) - 1, i]],new Vector3 (xpos + (size*8), ypos, 0));
			ObsOnLvl [gm.GetLength (0) - 1, i].transform.position = new Vector3 (xpos + (size*8), ypos, 0);
			ypos -= size;
		}
	}


	public IEnumerator AllStackRemove()
	{
		
		PoolManager.init (GPool.transform);
		yield return new WaitForSeconds (0f);

		var Step = StepOfGeneration < StepToDeleting + 1 ? StepToDeleting + 1 : StepOfGeneration;
	
		for (var i = 0; i <= 5; i++) {
			StartCoroutine (StackDeleting (Step));
			Step++;
			yield return new WaitForSeconds (0.2f);
		}

		for (var i = GPool.transform.childCount-1; i > 0; i--)
			if (!GPool.transform.GetChild (i).gameObject.activeInHierarchy)
				Destroy(GPool.transform.GetChild (i).gameObject);

		yield break;
	}
	public void ToStart()
	{
		xpos = startxpos;
		ypos = startypos;
		Counters.AllCounters["NowLvl"] = 0;
		StepOfGeneration = 0;
		GenDone = true;
	}
}

/*
 * Игра начинается
 * n = 25
 * k = 0
 * m = 5
 * Начало цикла
 * Генератор составляет массив на n*k>>n*(k+1) уровни
 * Генератор раставляет объекты на n*k>>n*(k+1) уровни
 * Генератор отключается?
 * Когда игрок достигает уровня (n*(k-1))-m, k++
 * Генератор включается?
 * Повтор пока 
 * 
 * */