using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Store : MonoBehaviour {

    //private bool touch;
    private int lastWindow;
	private Dictionary<string,float[]> size = new Dictionary<string,float[]>();
	private Dictionary<int,Sprite> skin = new Dictionary<int,Sprite>();
    private GameObject upgrades, ground, button, skins;
	private CameraManager cam;

	public int state;

	public void sendSkin(int index, Sprite sprite)
	{
        skin.Add(index,sprite);
	}

	public Sprite getSkin(int index)
	{
	  Debug.Log("got");
      return skin[index];
	}
	
	
	void Start () {

		 lastWindow = 0;
         cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();

         skins    = transform.GetChild(3).gameObject;
		 upgrades = transform.GetChild(4).gameObject;
		 ground   = transform.GetChild(5).gameObject;
		 button   = transform.GetChild(6).gameObject;

		 size.Add(upgrades.name,new float[2] {
	           upgrades.GetComponent<RectTransform>().offsetMax.y, +
			   upgrades.GetComponent<RectTransform>().offsetMin.y, 
 											 });
		 size.Add(ground.name,new float[2] {
			   ground.GetComponent<RectTransform>().offsetMax.y, 
			   ground.GetComponent<RectTransform>().offsetMin.y, 
											 });

		 button.GetComponent<StoreButtons>().SetValue(upgrades, ground);

         StartCoroutine(resize(-1,size[upgrades.name], upgrades));
		 StartCoroutine(resize(-1,size[ground.name], ground));
	}
	
	IEnumerator resize(int dir, float[] array, GameObject someObject){
		float steps = 25;
		var distance = new float[2];
        
        distance[0] =  size[someObject.name][0]/steps * dir;
		distance[1] =  size[someObject.name][1]/steps * dir;
	
		for(var i = 0; i<steps; i++ ){ 
			someObject.GetComponent<RectTransform>().offsetMax += new Vector2(0,distance[0]);
			someObject.GetComponent<RectTransform>().offsetMin += new Vector2(0,distance[1]);
			yield return new WaitForSeconds(0.01f);
		}
	}

		

	public void Check()
	{
		switch (state)
		{
			case 1:
				lastWindow = 1; 
				StartCoroutine(resize(1,size[upgrades.name], upgrades)); 
				ground.SetActive(false);    
				state = 0;
				break;
			case 2:
				lastWindow = 2; 
				StartCoroutine(resize(1,size[ground.name], ground)); 
				upgrades.SetActive(false); 
				state = 0;
				break;
			case 3:
				switch (lastWindow)
				{
					case 1:
						StartCoroutine(resize(-1,size[upgrades.name], upgrades)); 
						lastWindow = 0; 
						ground.SetActive(true);
						break;
					case 2:
						StartCoroutine(resize(-1,size[ground.name], ground));	
						lastWindow = 0;  
						upgrades.SetActive(true);
						break;
					case 0:
						cam.nowTarget = 1;
						break;
				}
				state = 0;
				break;
		}
	}
}
