using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextControllerCounting : Counters
{
    public TextMeshProUGUI Record;
    public TextMeshProUGUI Current;
    public static TextControllerCounting Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }

    public void SetValue()
    {
        Record.text = PreviousRecord.ToString();//AllCounters["MaxDistance"].ToString();
        Current.text = AllCounters["NowDistance"].ToString();
    }
}
