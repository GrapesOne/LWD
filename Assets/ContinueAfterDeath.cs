
using UnityEngine;

public class ContinueAfterDeath : Counters
{
    public GameObject PauseObject;
    public GameObject DeathObject;
    private UnityEngine.UI.Button Pausebutton;
    private UnityEngine.UI.Button CountinueButton;
    private PauseBotton pauseBotton;

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) OnButtonAction();
    }

#endif

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
        System.GC.Collect();
        Сontinue();
        CameraManager.Instance.nowTarget = 1;
        AudioManager.Play();
        pauseBotton.Pause();
        DeathObject.SetActive(false);
    }
}
