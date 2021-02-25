using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEntity : NotPlayableEntity
{
    private static int id;
    protected GroundAnimator Animator;
    public override void FillObject(GameObject ob)
    {
        ob.layer = 8;
        //ob.transform.position = new Vector3(ob.transform.position.x,ob.transform.position.y,0.1f);
        var components = ob.GetComponent<GameObjectInfo>().Components;
        if (!components.Contains("BoxCollider2D"))
        {
            components.Add("BoxCollider2D");
            var collider = ob.AddComponent<BoxCollider2D>();
            collider.size = Vector2.one;
        }

        if (!components.Contains("GroungAnimation"))
        {
            components.Add("GroungAnimation");
            var ga = ob.AddComponent<GroungAnimation>();
            ga.id = id++ % 30;
        }

        if (!components.Contains("SpriteRenderer"))
        {
            components.Add("SpriteRenderer");
            ob.GetComponent<SpriteRenderer>().sprite = sprite;
        }
        
    }
}
