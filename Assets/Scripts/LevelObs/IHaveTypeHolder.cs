using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public abstract class IHaveTypeHolder : SerializedMonoBehaviour
{
    [SerializeField] protected GameObject _genOb;
    [SerializeField] protected Vector2 _genObSize;
    [OdinSerialize, HideReferenceObjectPicker, HideLabel, FoldoutGroup("Types")]
    public List<EntityHolder> _holders = new List<EntityHolder>();
    protected readonly List<LevelBase> _bases = new List<LevelBase>();
    protected bool _hasBases;
    protected LevelBase _levelBase;
    protected int _nowBase;
    protected int x, y;
    public string BaseName => _bases[_nowBase]?.name;
    
    public virtual void LoadBases()
    {
        _bases.Clear();
        _bases.AddRange( Resources.LoadAll(Paths.ResourcesBases).Cast<LevelBase>().ToArray());
        _hasBases = _bases.Count > 0;
        _nowBase = 0;
        if (_hasBases) _levelBase = _bases[_nowBase];
    }
}