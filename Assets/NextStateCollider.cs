using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStateCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        AudioManager.SetGameAudio();
        CameraManager.Instance.nowTarget = 1;
        AnimationController.ShowInterface();
    }
}
