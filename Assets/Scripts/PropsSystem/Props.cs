using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class Props
{
    [OdinSerialize] private List<PropBase> _props = new List<PropBase>();
    public void Init() => _props.ForEach(prop => prop.SetType());

    public T GetValue<T>()
    {
        var propBase = _props.Find(prop => prop.type == typeof(T));
        if (propBase != null) return (T) propBase.GetValue();
        return default;
    }

    public object GetValue(Type T)
    {
        var propBase = _props.Find(prop => prop.type == T);
        return propBase?.GetValue();
    }

    [Button]
    public void DebugProps()
    {
        Init();
        foreach (var propBase in _props) Debug.Log(propBase.type.ToString());
        Debug.Log(GetValue<Color>());
        Debug.Log(GetValue<AnimationCurve>());
        Debug.Log(GetValue<Vector2>());
        var sprite = GetValue(typeof(Sprite));
        if(sprite != null) Debug.Log((Sprite)sprite);

    }
}
