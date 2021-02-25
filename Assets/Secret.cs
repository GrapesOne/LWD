using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GooglePlayGames;
using UnityEngine;

public class Secret : MonoBehaviour
{
    public GameObject friend;
    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.layer != 9) return;
        if (PlayGamesPlatform.Instance.IsAuthenticated())
            Social.ReportProgress("CgkIlNG-1bEUEAIQAQ", 100.0f, (bool success) => {});
        AudioManager.Instance.SetSecret();
        friend.SetActive(false);
        WaitForEnable();
    } 
    
    async UniTaskVoid WaitForEnable()
    {
        await UniTask.Delay(20000);
        friend.SetActive(true);
    }
}
