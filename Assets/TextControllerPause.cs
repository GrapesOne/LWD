using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextControllerPause : MonoBehaviour
{
    public TextMeshProUGUI CrystalText;
    public TextMeshProUGUI SphereText;
    private void OnEnable()
    {
        CrystalText.text=Counters.AllCounters["Crystals"].ToString();
        SphereText.text= Counters.AllCounters["Sphere"].ToString();
    }
}
