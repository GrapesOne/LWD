using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class _DataTreatment : MonoBehaviour
{
    private _Data data;
    private BinaryFormatter formatter;
    public bool save, show;
    public Hashtable test;
    
    void Start()
    {
         data = new _Data();
         formatter = new BinaryFormatter();
         loadData("data.wgd");
       /*save = false ;
         show = false;
         test =  new Hashtable();
         test.Add(1,"C - до");
         test.Add(2,"D - ре");
         test.Add(3,"E - ми");
         test.Add(4,"F - фа");
         test.Add(5,"G - соль");
         test.Add(6,"A - ля");
         test.Add(7,"B - си");
         test.Add(8,"C - до"); */
    } 

    void Update()
    {
       /*if(save)
         {
            save = false;
            clearData();
            data.ListOfObject = test;
            uploadData("data.wgd", "добавление новых элементов");
            Debug.Log("save done");
         }
         if(show)
         {
            show = false;
            loadData("data.wgd");
            for(int i = 1; i<=8; i++)
            {
               Debug.Log((string)data.ListOfObject[i]);
            }
         }*/
    }

    public void loadData(string name)
    {
           using(FileStream fs = new FileStream (name, FileMode.OpenOrCreate ))
           {
               data = (_Data)formatter.Deserialize(fs);
               Debug.Log("Объект десериализован");
           }
    }

    public void uploadData(string name, string message = " ")
    {
           using (FileStream fs = new FileStream (name, FileMode.OpenOrCreate))
           {
               formatter.Serialize( fs, data );
               Debug.Log("Объект сериализован: "+message);
           }
    } 

    public void putData(object res1, object res2)
    {
       if(data.ListOfObject.ContainsKey(res1))  data.ListOfObject.Remove(res1);  
       data.ListOfObject.Add(res1, res2);
    }
    
    public object getData(object res)
    {
       if(data.ListOfObject.ContainsKey(res))
       {
          return  data.ListOfObject[res];
       }
       else return null;
    } 

    public void clearData()
    {
       data.ListOfObject = null;
       uploadData("data.wgd", "очистка завершина");
    } 
}
