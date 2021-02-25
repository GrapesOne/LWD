using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.ProceduralImage;

public class TimeManager : Counters {
	public static TimeManager Instance { private set; get; }
	private ProceduralImage _img;
	//public GameObject Counter;
	//[SerializeField] public Counters Count;
	[Space]
	//public Text txt;
	private int TimeCount;
	float timer = 1;
	private CameraManager Cam;
	void Awake()
	{
		Instance = this;
		_img = GetComponent<ProceduralImage>();
	}
	void Start()
	{
		Cam = CameraManager.Instance;
	}


	public void DownTime()
	{
		TimeCount -= AllCounters["EnemyTime"];
	}
	public void TimeUp()
	{
		TimeCount += AllCounters["ClockTime"];
	}
	public void TimeUpContinue()
	{
		TimeCount += 10;
	}
	
	
	void Update () {
		//txt.text = TimeCount.ToString ();
		_img.fillAmount = TimeCount/60f;
		switch (Cam.nowTarget)
		{
			case 0:
				TimeCount = AllCounters["StartTime"];
				break;
			case 1:
			{
				if (timer >= 0)
					timer -= Time.deltaTime;
				else
				{
					timer = 1;
					TimeCount--;
					AllCounters["SecondSpent"]++;
				}

				if (TimeCount <= 0)
				{
					DeathScreen.Instance.EnableMenu();
					//ExitButton.OnButtonStaticClick();
					//Cam.nowTarget = 5;
				}

				break;
			}
		}
	}

}
