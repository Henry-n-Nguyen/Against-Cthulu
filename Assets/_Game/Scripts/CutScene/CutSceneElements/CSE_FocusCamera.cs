using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_FocusCamera : CutSceneElementBase
{
    [SerializeField] private GameObject focusPanel;
    [SerializeField] private Animator anim;

    private Coroutine focusCameraCoroutine;

    public override void Excecute()
    {
        focusPanel.SetActive(true);
        anim.Play("Execute");
        focusCameraCoroutine = StartCoroutine(WaitAndAdvance());
    }

    public override void Release()
    {
        StopCoroutine(focusCameraCoroutine);
        anim.Play("Release");
    }
}
