using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyEntity : NotPlayableEntity
{
    public override void FillObject(GameObject ob)
    {
        //ob.name = EntityType.ToString();
        ob.GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
