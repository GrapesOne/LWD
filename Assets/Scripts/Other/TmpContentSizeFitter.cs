using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using TMPro;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class TmpContentSizeFitter : SerializedMonoBehaviour
{
    [SerializeField] private bool UseX = true, UseY = true;
    [SerializeField] private Vector2 charSizeConst = new Vector2(0.575f, 0.6f);
    [SerializeField][TypeFilter("GetFilteredTypeList")] 
    private TmpSizeType sizeTypeX  = new charSize();
    
    public IEnumerable<Type> GetFilteredTypeList()
    {
        var q = typeof(TmpSizeType).Assembly.GetTypes()
            .Where(x => !x.IsAbstract)
            .Where(x => typeof(TmpSizeType).IsAssignableFrom(x));
        return q;
    }
    private Vector2 _contentSize = new Vector2();
   
    private RectTransform _transform;
    private TextMeshProUGUI _tmp;

    [Button("CalculateContentSize")]
    public void CalculateContentSizeButton()
    {
        _transform = GetComponent<RectTransform>();
        _tmp = GetComponent<TextMeshProUGUI>();
        sizeTypeX.Init(_tmp,charSizeConst);
        CalculateContentSize();
    }
    async UniTaskVoid Start()
    {
        _transform = GetComponent<RectTransform>();
        _tmp = GetComponent<TextMeshProUGUI>();
        sizeTypeX.Init(_tmp,charSizeConst);
        await UniTask.Delay(100);
        CalculateContentSize();
    }
    
    public void CalculateContentSize()
    {
        _contentSize.x = sizeTypeX.value();
        _contentSize.y = Mathf.Floor(_tmp.text.Length * _tmp.fontSize * charSizeConst.x / (int)_contentSize.x + 1 ) * _tmp.fontSize *
                         charSizeConst.y;
        _transform.sizeDelta = new Vector2(UseX?_contentSize.x:_transform.rect.width, UseY?_contentSize.y:_transform.rect.height);
    }



}
public abstract class TmpSizeType
{
    protected TextMeshProUGUI _tmp;
    protected Vector2 _charSizeConst = new Vector2(0.575f, 0.6f);
    public void Init(TextMeshProUGUI tmp, Vector2 charSizeConst)
    {
        _tmp = tmp;
        _charSizeConst = charSizeConst;
    }

    public abstract float value();
}
public class charSize : TmpSizeType
{
    [SerializeField] private int charCount = 100;
    public override float value()=> (_tmp.text.Length/charCount == 0? _tmp.text.Length :charCount)* _tmp.fontSize * _charSizeConst.x;
}
public class Width : TmpSizeType
{
    [SerializeField] private float width = 100;
    private float res;
    public override float value()
    {
        res = _tmp.text.Length * _tmp.fontSize * _charSizeConst.x;
        return res / width < 1 ? res : width;
    }
}
public class Constant : TmpSizeType
{
    public override float value()
    {
        return _tmp.GetComponent<RectTransform>().rect.width;
    }
    
}
