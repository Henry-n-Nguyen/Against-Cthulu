using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSE_Test : CutSceneElementBase
{
    public override void Excecute()
    {
        StartCoroutine(WaitAndAdvance());
        Debug.Log("Excecute CS...");
    }

    public override void Release()
    {
        Debug.Log("Excecute CS...");
    }
}
