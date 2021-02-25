using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public abstract class EntityHolder
{
    public Color color;
    public abstract GameObject CreateOb(GameObject prefab, Vector3 v, int additional = 0);
}

public class EmptyEH : EntityHolder {
    public override GameObject CreateOb(GameObject prefab, Vector3 v, int additional = 0) => null;
}

public abstract class NotEmptyEH : EntityHolder
{
    public NotPlayableEntity[] entities = new NotPlayableEntity[0];
}
public class ObligatoryEH : NotEmptyEH {
    public override GameObject CreateOb(GameObject prefab, Vector3 v, int additional = 0)
    {
        var inst = PoolManager.getGameObjectFromPool(prefab, v, entities[0].EntityType);
        if (entities.Length != 0) entities[0].FillObject(inst);
        //inst.GetComponent<SpriteRenderer>().color = color;
        return inst;
    }
}
public class OnceAdjustableEH : NotEmptyEH {
    [Range(1, 2)] public int Group;
    public override GameObject CreateOb(GameObject prefab, Vector3 v, int additional = 0)
    {
        if (additional != Group) return null;
        var inst = PoolManager.getGameObjectFromPool(prefab, v, entities[0].EntityType);
        if (entities.Length != 0) entities[0].FillObject(inst);
        //inst.GetComponent<SpriteRenderer>().color = color;
        return inst;
    }
}
public class ProbabilisticEH : NotEmptyEH
{
    [Range(0f, 1f)] public float probability;
    public override GameObject CreateOb(GameObject prefab, Vector3 v, int additional = 0)
    {
        if (Random.Range(0f, 1f) > probability) return null;
        var inst = PoolManager.getGameObjectFromPool(prefab, v, entities[0].EntityType);
        if (entities.Length != 0) entities[0].FillObject(inst);
        //inst.GetComponent<SpriteRenderer>().color = color;
        return inst;
    }
}
/*
public class  OnceAdjustableProbabilisticEH : NotEmptyEH
{
    [Range(0f, 1f)] public float probability;
    public override GameObject CreateOb(GameObject prefab, Vector3 v, bool additional = true)
    {
        if (!additional  && Random.Range(0f, 1f) > probability) return null;
        var inst = PoolManager.getGameObjectFromPool(prefab, v);
        if (entities.Length != 0) entities[0].FillObject(inst);
        //inst.GetComponent<SpriteRenderer>().color = color;
        return inst;
    }
}
*/