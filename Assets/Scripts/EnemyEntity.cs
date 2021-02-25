using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : NotPlayableEntity
{
    public LayerMask LayerMask;
    public override void FillObject(GameObject ob)
    {
        var components = ob.GetComponent<GameObjectInfo>().Components;
        if (!components.Contains("EnemyBonus"))
        { 
            components.Add("EnemyBonus");
            var eb = ob.AddComponent<EnemyBonus>();
            eb.WhatIsGround = LayerMask;
        }
        if (!components.Contains("BoxCollider2D"))
        {
            components.Add("BoxCollider2D");
            var box = ob.AddComponent<BoxCollider2D>();
            box.isTrigger = true; 
            box.size = Vector2.one*0.3f;
        }
        if (!components.Contains("SpriteRenderer"))
        {
            components.Add("SpriteRenderer");
            ob.GetComponent<SpriteRenderer>().sprite = sprite;
        }


    }
}
