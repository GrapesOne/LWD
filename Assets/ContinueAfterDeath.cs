
using UnityEngine;

public class ContinueAfterDeath : Counters
{
    public GameObject PauseObject;
    public GameObject DeathObject;
    private UnityEngine.UI.Button Pausebutton;
    private UnityEngine.UI.Button CountinueButton;
    private PauseBotton pauseBotton;

    private void Awake()
    {
        CountinueButton = gameObject.GetComponent<UnityEngine.UI.Button>();
        Pausebutton = PauseObject.GetComponent<UnityEngine.UI.Button>();
        pauseBotton = PauseObject.GetComponent<PauseBotton>();
    }
    private void OnEnable()
    {
        CountinueButton.interactable = CheckSphere();
    }

    public void OnButtonAction()
    {
        Сontinue();
        CameraManager.Instance.nowTarget = 1;
        AudioManager.Play();
        pauseBotton.Pause();
        DeathObject.SetActive(false);
    }
}
