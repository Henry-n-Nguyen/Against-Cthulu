using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using HuySpace;

public class CutSceneUIManager : Singleton<CutSceneUIManager>
{
    private CutSceneUI[] uis;

    private void Start()
    {
        uis = GetComponentsInChildren<CutSceneUI>();
    }

    public void Execute(CS_UIType type)
    {
        foreach (CutSceneUI ui in uis)
        {
            if (ui.type == type)
            {
                ui.gameObject.SetActive(true);
                ui.Execute();
                return;
            }
        }
    }

    public void Release(CS_UIType type)
    {
        foreach (CutSceneUI ui in uis)
        {
            if (ui.type == type)
            {
                ui.Release();
                return;
            }
        }
    }
}
