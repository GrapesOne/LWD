using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreButtons : MonoBehaviour {
    
	private Store main; 
	private delegate void Action();
	private Action action;
	private BoxCollider2D[] boxcol = new BoxCollider2D[3];
	


	void Start () {
		 
		 boxcol[0] = gameObject.GetComponent<BoxCollider2D>();
		 main   = gameObject.GetComponentInParent<Store>();
		 action = main.Check; 
         
	}

	void OnMouseUp(){
		 switch(gameObject.name){
			 case "upgrades":   main.state = 1; action(); boxcol[0].enabled = false; break; 
			 case "ground":     main.state = 2;  Debug.Log(main.state); action(); boxcol[0].enabled = false; break;
			 case "backButton": main.state = 3; action();
			                    boxcol[1].enabled = true;
								boxcol[2].enabled = true;
			                    break;
		 }
	}
	
	public void SetValue(GameObject a, GameObject b){
        boxcol[1] = a.GetComponent<BoxCollider2D>();
		boxcol[2] = b.GetComponent<BoxCollider2D>();
	}


}
