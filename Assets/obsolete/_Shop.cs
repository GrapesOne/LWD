using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class _Shop : MonoBehaviour
{
       public event EventHandler<argsOfShop> Press;
       private argsOfShop args;
       public GameObject fff;

       void Start()
       {
            object tmp = null;
            args = new argsOfShop();
            args.admin = GameObject.FindGameObjectWithTag("Admin").GetComponent<_DataTreatment>();
            
            try{
                  tmp = args.admin.getData("cash");
               }
            catch( Exception e )
            {
                  Debug.Log("Рано \n"+e.Message);
            }
            
            if(tmp == null) { args.cash = 0; }
            else args.cash = (int)tmp;
       }  

       public void Call()
       {
            if(Press!=null)
            {
               Press(this, args); 
            }
       }
}
