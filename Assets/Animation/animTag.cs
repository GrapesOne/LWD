using UnityEngine;

public class animTag : MonoBehaviour
{
    private bool _enabled;
    public void SetActive()
    {
        foreach (Transform t in transform) t.gameObject.SetActive(_enabled);
        _enabled = !_enabled;
    }

}
