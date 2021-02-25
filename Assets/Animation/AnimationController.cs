using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private static Animator animationer;
    private static readonly int Show = Animator.StringToHash("Show");
    private static animTag _animTag;
    private void Awake()
    {
        _animTag = transform.GetComponentInChildren<animTag>();
        animationer = GetComponent<Animator>();
    }
    public static void ShowInterface() => animationer.SetBool(Show, true);
    public static void HideInterface() => animationer.SetBool(Show, false);
    public static void ForcedUsingInterface() => _animTag.SetActive();
}
