using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonOff : MonoBehaviour
{
    public static ButtonOff Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }
    public void ButtonOn()
    {
        gameObject.SetActive(true);
    }
    public void ButtonOffs()
    {
        gameObject.SetActive(false);
    }
}
