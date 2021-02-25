#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.UI;


public class TypeColorHolder : MonoBehaviour
{
    private Image _image;
    [SerializeField]private int _type;
    private UnityEngine.UI.Button _button;
    public void SetColor(int t, Color c)
    {
        _type = t;
        _image = GetComponent<Image>();
        _image.color = c;
        _button = GetComponent<UnityEngine.UI.Button>();
        _button.onClick.AddListener(()=>{LevelShow.Main.SetColorType(_type);});
    }

    void OnEnable()
    {
        _button = GetComponent<UnityEngine.UI.Button>();
        _button.onClick.AddListener(()=>{LevelShow.Main.SetColorType(_type);});
    }

}
#endif