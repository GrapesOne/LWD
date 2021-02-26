#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.Scripting;

public class FrameStop : MonoBehaviour
{

    public bool LOCK = true;
    [Range(15,60)]public int frameRate = 60;

    void OnEnable()
    {
        /*
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
		
		*/
       // QualitySettings.vSyncCount = 0; 
        GarbageCollector.GCMode = GarbageCollector.Mode.Disabled;
        System.GC.Collect();
    }
    void OnDisable()
    {
        GarbageCollector.GCMode = GarbageCollector.Mode.Enabled;
        System.GC.Collect();
    }
    
  /* 
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
    */
}

#endif
