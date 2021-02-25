using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnButtonClick()
    {
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
