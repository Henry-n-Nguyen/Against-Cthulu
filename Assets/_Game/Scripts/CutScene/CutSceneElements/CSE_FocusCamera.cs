using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class CSE_FocusCamera : CutSceneElementBase
{
    private Coroutine focusCameraCoroutine;

    public override void Execute()
    {
        CutSceneUIManager.Ins.Execute(CS_UIType.FocusCam);
        focusCameraCoroutine = StartCoroutine(WaitAndAdvance());
    }

    public override void Release()
    {
        StopCoroutine(focusCameraCoroutine);
        CutSceneUIManager.Ins.Release(CS_UIType.FocusCam);
    }
}
