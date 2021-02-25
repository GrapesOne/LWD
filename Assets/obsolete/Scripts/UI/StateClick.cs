using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateClick : INeedCam   {
	
	
	[SerializeField][Range(0,6)] int State = 0;

	void OnMouseDown()
	{
		FoundCam ();
		StateToCamera (State);
	}

}
