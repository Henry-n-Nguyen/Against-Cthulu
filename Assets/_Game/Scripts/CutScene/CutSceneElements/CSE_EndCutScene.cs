using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_EndCutScene : CutSceneElementBase
{
    public override void Excecute()
    {
        cutsceneHandler.ReleaseAllElement();
        if (cutsceneHandler.isOneTrigger) gameObject.SetActive(false);
    }

    public override void Release()
    {
        base.Release();
    }
}
