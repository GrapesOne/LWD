using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class PanelColorPicker : SerializedMonoBehaviour
{
    [OdinSerialize] private List<ColorGroup> ColorGroups = new List<ColorGroup>();
    private UIGradient graphicGradient;
    struct ColorGroup
    {
        public Color color1;
        public Color color2;
        [Range(-180f, 180f)]public float Angle;
        [Range(0f, 1f)] public float Alpha ;
        [OdinSerialize] public List<Graphic> targetGraphic;
    }
    [Button("Pick")]
    public void Pick()
    {
        foreach (var colorGroup in ColorGroups)
        foreach (var graphic in colorGroup.targetGraphic)
        {
            graphicGradient = graphic.GetComponent<UIGradient>();
            if (graphicGradient == null)
                graphicGradient = graphic.gameObject.AddComponent<UIGradient>();
            graphic.color = new Color(1,1,1,colorGroup.Alpha);
            graphicGradient.mColor1 = colorGroup.color1;
            graphicGradient.mColor2 = colorGroup.color2;
            graphicGradient.Angle = colorGroup.Angle;

        }
            
    }
}
