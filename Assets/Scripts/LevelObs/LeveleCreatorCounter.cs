#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeveleCreatorCounter : MonoBehaviour
{
    private Text _text;

    void Start()
    {
        _text = GetComponentInChildren<Text>();
    }
    void Update()
    {
        _text.text = LevelShow.Main.BaseName;
    }
}
#endif