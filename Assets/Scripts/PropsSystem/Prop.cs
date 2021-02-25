using UnityEngine;

public abstract class Prop<T> : PropBase
{
   [SerializeField] private T _prop;
   public override void SetType() => type = typeof(T);
   public override object GetValue() => _prop;
}
