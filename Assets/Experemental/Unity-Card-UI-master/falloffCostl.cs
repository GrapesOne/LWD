using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
public class falloffCostl : MonoBehaviour
{
    [Range(0, 50)] public float falloff;
    SquircleImage[] _images = new SquircleImage[8];
    private Vector2[] _masks =
    {
        new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1),
        new Vector2(-1, 1), new Vector2(1, 0), new Vector2(-1, 0),
        new Vector2(0, 1), new Vector2(0, -1)
    };
    void OnValidate()
    {
        for (var i = 0; i < 8; i++)
        {
            _images[i] = transform.GetChild(i).GetComponent<SquircleImage>();
            _images[i].ExtraPos = falloff * _masks[i];
            _images[i].HandlOnValidate();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
#endif