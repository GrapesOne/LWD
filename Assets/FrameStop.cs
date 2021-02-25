#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.LowLevel;

public class FrameStop : MonoBehaviour
{

    public bool LOCK = true;
    [Range(15,60)]public int frameRate = 60;
    
   
    void OnValidate()
    {
        Time.fixedDeltaTime = 0.02f*Mathf.Pow(frameRate/60f, 10);
        if (LOCK)
        {
            QualitySettings.vSyncCount = 0; 
            Application.targetFrameRate = frameRate;
        }
        else
        {
            QualitySettings.vSyncCount = 1;
            Application.targetFrameRate = 20000;
        }
            
    }
    
}

#endif
