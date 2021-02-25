#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellOb : MonoBehaviour
{
    public int _type;
    private SpriteRenderer _renderer;

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }
    void Click()
    {
        _type = LevelShow.Main.ColorType;
        SetColor();
    }

    void OnMouseDown() => Click();
    void OnMouseEnter()
    {
        if(Input.GetMouseButton(0)) Click();
    }


    public void SetColor()=>_renderer.color = LevelShow.Main._holders[_type].color;
}
#endif