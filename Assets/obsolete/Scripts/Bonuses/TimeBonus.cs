using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBonus : Counters {

	protected override void ActBonus()
	{
		TimeManager.Instance.TimeUp();
	}
}
