using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) OnButtonClick();
    }

#endif

    public void OnButtonClick()
    {
        MainMenuCanvas.Instance.gameObject.SetActive(true);
        System.GC.Collect();
        DeathScreen.Instance.DisableMenu();
        AnimationController.HideInterface();
        CameraManager.Instance.nowTarget = 0;
        AudioManager.SetMainMenuAudio();
        AudioManager.Play();
        Player.EventReturn();
        Distance.Instance.Reset();
        Generator.Instance.Reset();
        
    }   
    
}
