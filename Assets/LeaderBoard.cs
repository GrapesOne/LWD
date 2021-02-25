using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{
    public GameObject SignIN;
    private void Awake()
    {
        if (PlayGamesPlatform.Instance.IsAuthenticated())
        {
            ButtonON();
        }
        else ButtonOFF();
    }
    public void OnButtonLeaders()
    {
            PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkIlNG-1bEUEAIQAg");
    }
    public void OnButtonAchievs()
    {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
    public void OnButtonSignIN()
    {
        PlayGamesPlatform.Instance.Authenticate(callback=> { 
               if(callback)
                ButtonON(); 
        });
    }
    public void OnButtonSignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
        ButtonOFF();
    }
    private void ButtonON()
    {
        foreach (Transform obj in transform)
        {
            obj.gameObject.SetActive(true);
        }
        SignIN.SetActive(false);
    }
    private void ButtonOFF()
    {
        foreach (Transform obj in transform)
        {
            obj.gameObject.SetActive(false);
        }
        SignIN.SetActive(true);
    }
}
