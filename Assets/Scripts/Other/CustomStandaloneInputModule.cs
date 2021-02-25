using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
 
/*  /class CustomStandaloneInputModule
*  /brief A StandaloneInputModule with access to buttons' PointerEventData and RaycasterModule checking
*/
public class CustomStandaloneInputModule : StandaloneInputModule
{
    static CustomStandaloneInputModule origin;
    protected override void Awake()
    {
        base.Awake();
        origin = gameObject.GetComponent<CustomStandaloneInputModule>();
    }

    public static ref readonly CustomStandaloneInputModule InputModule() => ref origin;

    /// <summary>
    /// Returns current PointerEventData
    /// </summary>
    public PointerEventData GetPointerData(int pointerId = kMouseLeftId)
    {
        PointerEventData pointerData;
 
        m_PointerData.TryGetValue(pointerId, out pointerData);
        if (pointerData == null) pointerData = new PointerEventData(EventSystem.current);
 
        return pointerData;      
    }
 
    /// <summary>
    /// Returns true if current raycast has hit an game object using Raycaster T
    /// </summary>
    public bool IsPointerOverGameObject<T>(int pointerId = kMouseLeftId, bool includeDerived = true) where T : BaseRaycaster
    {
        if(IsPointerOverGameObject(pointerId))
        {
            PointerEventData pointerEventData;
            if (m_PointerData.TryGetValue(pointerId, out pointerEventData))
            {
                if (includeDerived)
                {
                    Debug.Log(pointerEventData.pointerCurrentRaycast.module.name);
                    return pointerEventData.pointerCurrentRaycast.module is T;
                }
                else
                {
                    Debug.Log(pointerEventData.pointerCurrentRaycast.GetType());
                    return pointerEventData.pointerCurrentRaycast.module.GetType() == typeof(T);
                }
            }
        }
        return false;
    }
}