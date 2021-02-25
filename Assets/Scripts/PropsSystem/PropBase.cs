using System;
public abstract class PropBase
{
    public Type type { get; protected set; }
    public abstract void SetType();
    public abstract object GetValue();
}
