#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
 
public class CommentAttribute : PropertyAttribute {
    public readonly string tooltip;
    public readonly string comment;
    public readonly int height;
     
    public CommentAttribute( string comment, string tooltip, int height = 20 ) {
        this.tooltip = tooltip;
        this.comment = comment;
        this.height = height;
    }
}
 
[CustomPropertyDrawer(typeof(CommentAttribute))]
public class CommentDrawer : PropertyDrawer {
    
    CommentAttribute commentAttribute { get { return (CommentAttribute)attribute; } }
     
    public override float GetPropertyHeight(SerializedProperty prop, GUIContent label) {
        return commentAttribute.height;
    }
     
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {
        EditorGUI.LabelField(position,new GUIContent(commentAttribute.comment,commentAttribute.tooltip));
    }
}

 
#endif