using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IClickableBotton : INeedCam {

	protected bool Check(int State)
	{
		FoundCam();
		var ret = Cam.nowTarget == State;
		return  ret;
	}
}
