using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectInfo : MonoBehaviour
{
    public GameObject WallLeft;
    public GameObject WallBottom;
    public GameObject Point;

    private void Awake()
    {
        WallLeft = transform.GetChild(0).gameObject;
        WallBottom = transform.GetChild(1).gameObject;
        Point = transform.GetChild(2).gameObject;
    }

    public List<string> Components = new List<string>();
}
