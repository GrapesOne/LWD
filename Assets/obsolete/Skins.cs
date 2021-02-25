using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skins : MonoBehaviour {

             public List<GameObject> skinList = new List<GameObject>();

			 void Start(){
				  int i = skinList.Count;
				  float d = gameObject.GetComponent<Transform>().position.x-6;
				  foreach(GameObject a in skinList){
				  a.GetComponent<skin>().Index = i ;
				  Instantiate(a,new Vector3(d,gameObject.GetComponent<Transform>().position.y,0),
				  Quaternion.identity,gameObject.GetComponent<Transform>());
				  d+=1;
				  i--;
				  }
			 }

			
}
