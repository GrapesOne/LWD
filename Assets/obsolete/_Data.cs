using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

[Serializable]
public class _Data 
{
    public Hashtable ListOfObject;
    
    public _Data()
    {
        ListOfObject = new Hashtable();
    }
}