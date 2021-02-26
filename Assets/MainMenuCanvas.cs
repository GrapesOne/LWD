using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvas : MonoBehaviour
{
    public static MainMenuCanvas Instance { private set; get; }
    void Awake()
    {
        Instance = this;
    }
    
}
