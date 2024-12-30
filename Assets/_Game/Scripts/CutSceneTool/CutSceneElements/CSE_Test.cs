using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_Test : CutSceneElementBase
{
    public override void Execute()
    {
        StartCoroutine(WaitAndAdvance());
        Debug.Log("Execute CS...");
    }

    public override void Release()
    {
        Debug.Log("Execute CS...");
    }
}
