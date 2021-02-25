using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject Screen;
    public TextMeshProUGUI Text;
    public static DeathScreen Instance { private set; get; }

    void Awake()
    {
        Instance = this;
    }
    
    public void EnableMenu()
    {
        Distance.Instance.SetupDistance();
        Text.text = Counters.SphereCost.ToString();
        CameraManager.Instance.nowTarget = 3;
        PauseBotton.Instance.Pause();
        Screen.SetActive(true);
        TextControllerCounting.Instance.SetValue();
    }

    public void DisableMenu()
    {
        Screen.SetActive(false);
    }
}
