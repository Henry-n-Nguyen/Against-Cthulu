using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_Shop : CutSceneElementBase
{
    private Coroutine waitCoroutine;

    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            cutsceneHandler.PlayNextElement();
        }
    }

    public override void Execute()
    {
        GamePlayManager.Ins.OnShop();
    }

    public override void Release()
    {
        if (waitCoroutine != null) StopCoroutine(waitCoroutine);
    }
}
