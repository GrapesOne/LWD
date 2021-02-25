using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour {
	
	private float     distance = 10f;                       //max distance between firts and last touch
	private int       move;                                 //state of move
	private Vector2   fpos, lpos;                           //first localPosition of mouse -- last postion  of mouse
	private Transform objPos;                               //objects Transform
	private int 	  numberOfItem;
	private int 	  state;
	private int 	  quanOfStates;
	private Transform objTransform;

    public GameObject Store;
	public GameObject nonPlayer;
    public float highPoint, lowPoint, speed, steps;      
	public GameObject objToMove; 

	void Start (){
		objPos = gameObject.GetComponent<RectTransform>();
		state = 6;
		 
		if(objToMove==null) 
		  objTransform = gameObject.GetComponent<Transform>();
		else 
		  objTransform = objToMove.GetComponent<Transform>();
    }

	void OnMouseDown(){
		fpos  = Input.mousePosition;
	}

	void OnMouseUp(){
		lpos  = Input.mousePosition;

		if(lpos.x > fpos.x && lpos.x - fpos.x > distance ) {
		  move = 1; 
		  quanOfStates = Mathf.CeilToInt((lpos.x - fpos.x)/100);
		} else if(lpos.x < fpos.x && fpos.x - lpos.x > distance ) {
           move =-1;
		   quanOfStates = Mathf.CeilToInt((fpos.x - lpos.x)/100);
		} else 
		   move = 0;
		StartCoroutine(Move(steps,speed,objTransform));
	}	
	
	IEnumerator Move(float steps, float time, Transform obj){
		        float dis = 0;
	
                if(state != 12 && move == 1){
					dis = quanOfStates;
					state+= quanOfStates;
				} else if(state != 1 && move ==-1){
					dis = quanOfStates;
					state-= quanOfStates;
				} 

				float oneStep = dis/steps * move;
	
				for(int b = 0; b < steps; b++ ){	
				 	    obj.localPosition += new Vector3(oneStep,0,0);
				 		yield return new WaitForSeconds(time); 
				 	}
                
				nonPlayer.GetComponent<Image>().sprite = Store.GetComponent<Store>().getSkin(state);
				Debug.Log(state);

		move = 0;
		yield break;
	}
}
