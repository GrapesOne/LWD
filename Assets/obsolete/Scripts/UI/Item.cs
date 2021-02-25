using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	public Sprite sprite;

	private Image icon;
	private Text info;

	void Start () 
	{
		icon = gameObject.transform.GetChild (0).GetComponent<Image> ();
		icon.sprite = sprite;
	}
}


