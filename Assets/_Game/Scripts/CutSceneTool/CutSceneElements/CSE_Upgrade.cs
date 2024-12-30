using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class CSE_Upgrade : CutSceneElementBase
{
    private Coroutine waitCoroutine;

    private void Update()
    {
        if (Input.GetButtonDown("Escape") && cutsceneHandler.isDetectedPlayer)
        {
            cutsceneHandler.PlayNextElement();
        }
    }

    public override void Execute()
    {
        GamePlayManager.Ins.OnUpgrade();
    }

    public override void Release()
    {
        if (waitCoroutine != null) StopCoroutine(waitCoroutine);
    }
}
