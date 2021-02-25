using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Vector3 mainPos, additionalPos, vz = new Vector3(0,0,-25);
    //void Update() => transform.position = mainPos + additionalPos+vz;
    public void PlusPosX(float x) => additionalPos.x += x;
    public void PlusPosY(float y) => additionalPos.y += y;
}
