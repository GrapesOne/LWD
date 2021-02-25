using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class skin : MonoBehaviour {

       private Sprite sprite;
       private Store store;
       public int index;

       public int Index
       {
           set { index = value; }
           get { return index;}
       }

       void Start()
       {
         store = GameObject.FindGameObjectWithTag("Store").GetComponent<Store>();
         //Debug.Log("dal");
         sprite = GetComponent<Image>().sprite;
         store.sendSkin(index,sprite);
       }
	  
}
