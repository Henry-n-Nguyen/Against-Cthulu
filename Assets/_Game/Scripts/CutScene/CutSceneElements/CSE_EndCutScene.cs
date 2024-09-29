using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_EndCutScene : CutSceneElementBase
{
    public override void Execute()
    {
        cutsceneHandler.ReleaseAllElement();
    }

    public override void Release()
    {
        base.Release();
    }
}
