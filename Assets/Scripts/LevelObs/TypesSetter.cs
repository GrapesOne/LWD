#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TypesSetter : MonoBehaviour
{
    public GameObject buttonPrefab;
    public static TypesSetter Main { get; private set; }

    void OnEnable()
    {
        Main = this;
    }
    public void SetColorsToChildren(IEnumerable<EntityHolder> colors)
    {
        var i = transform.childCount-1;
        for ( ;i>=0; i--) DestroyImmediate(transform.GetChild(i).gameObject);
        i = 0;
        foreach (var color in colors)
        {
            var child = Instantiate(buttonPrefab, transform);
            child.GetComponent<TypeColorHolder>().SetColor(i++, color.color);
        }
    }
}
#endif