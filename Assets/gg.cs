using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gg : MonoBehaviour
{
    private TextMeshProUGUI _gui;
    // Start is called before the first frame update
    void Start()
    {
        _gui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _gui.text = Time.deltaTime.ToString();
    }
}
