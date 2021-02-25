using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] MainMenu;
    public AudioClip[] Game;
    private AudioSource Source;
    private static bool inGame = false, stoped;
    public static AudioManager Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
        Source = GetComponent<AudioSource>();
    }
    public static void SetMainMenuAudio()
    {
        inGame = false;
    }
    public static void SetGameAudio()
    {
        inGame = true;
    }

    public void SetSecret()
    {
        if (Game[0] == Source.clip || MainMenu[0] == Source.clip)
            Source.clip = Game[1];
        else Source.clip = Game[0];
        inGame = true;
    }

    public static void Stop()
    {
        stoped = true;
    }
    public static void Play()
    {
        stoped = false;
    }
    private void Update()
    {
        if (stoped)
        {
            Source.Stop();
            return;
        }
        if (Source.isPlaying) return;
        Source.Play();
        
        
       
        if (inGame)
        {
            Source.clip = Game[Random.Range(0, Game.Length)];
            Source.Play();
        }
        else
        {
            Source.clip = MainMenu[Random.Range(0, MainMenu.Length)];
            Source.Play();
        }
    }
}
[Serializable]
public struct VolumedAudioClip
{
    public AudioClip clip;
    public float volume;
}
