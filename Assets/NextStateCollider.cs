using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NextStateCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        System.GC.Collect();
        AudioManager.SetGameAudio();
        CameraManager.Instance.nowTarget = 1;
        AnimationController.ShowInterface();
        Disable();
    }

    async UniTaskVoid Disable()
    {
        await UniTask.DelayFrame(25);
        MainMenuCanvas.Instance.gameObject.SetActive(false);
    }
}
