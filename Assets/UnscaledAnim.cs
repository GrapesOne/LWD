using UnityEngine;


public class UnscaledAnim : MonoBehaviour
{
  
    private Animator anim;
    private AudioSource aus;
    public VolumedAudioClip win, lose;
    public bool winGame;
    void Awake()
    {
        anim = GetComponent<Animator>();
        aus = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        winGame = Counters.IsNewRecord;
        AudioManager.Stop();
        Destroy(aus);
        aus = gameObject.AddComponent<AudioSource>();
        aus.clip = (winGame ? win : lose).clip;
        aus.volume = (winGame ? win : lose).volume;
        Time.timeScale = 0.1f;
        aus.Play();
        Time.timeScale = 0f;
        anim.SetBool("win", winGame);
    }

}
