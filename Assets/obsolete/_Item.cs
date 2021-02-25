using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class argsOfShop: EventArgs
{
       public int cash;
       public _DataTreatment admin;
}

public class _Item: MonoBehaviour
{
    public string _name;                // item name
    public Sprite sprite1, sprite2;     // object icons
    public string _attribute;           // attribute tha must be changed
    public int point;                   // increase/decrease value
    public int price;                   // how much it costs
    public int cash;                    // enogh cash

    public bool onEvent;

    private _Shop shop;

    void Start()
    {
         onEvent = false;
         shop = GameObject.FindGameObjectWithTag("Shop").GetComponent<_Shop>();
         shop.Press += new EventHandler<argsOfShop>(this.OnCashChanging);
    }

     /*void OnMouseUp()
      {
            if(onEvent)
          {
              onEvent = false;
              if(cash>=price) 
              {
                   Debug.Log(gameObject.name +" подписался");
                   shop.Press += new EventHandler<argsOfShop>( this.OnTest );
                   shop.Call();
              }   
          }
     } */

     void Update()
     {
          if(onEvent)
          {
              onEvent = false;
              if(cash>=price) 
              {
                   Debug.Log(gameObject.name +" подписался");
                   shop.Press += new EventHandler<argsOfShop>( this.OnTest );
                   shop.Call();
              }   
          }
     }
    

    void OnCashChanging(object sender, argsOfShop e)
    {
         //Sprite image;
         if( e.cash >= price ) 
         { 
           //image = sprite1;
           Debug.Log(gameObject.name +" зеленый");
         }
           else 
           {
                //image = sprite2;
                Debug.Log(gameObject.name +" красный");
           }
         //gameObject.GetComponent<Image>().sprite = image;
         cash = e.cash;
    }

    void OnBuying(object sender, argsOfShop e)
    {
         if(cash>=price)
         {
           float point  = (float)e.admin.getData(_attribute);
                 point += this.point;
           e.admin.putData(_attribute, point);
           e.admin.putData("cash",e.cash -= price);
         }
    }

    void OnTest(object sender, argsOfShop e)
    {
         Debug.Log(gameObject.name+" отписался");
         shop.Press -= new EventHandler<argsOfShop>( this.OnTest );
    }
}
