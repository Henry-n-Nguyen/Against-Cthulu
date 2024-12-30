using HuySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CSE_PlayCutScene : CutSceneElementBase
{
    [SerializeField] private CutSceneDataSO data;
    [SerializeField] private CutSceneType cs_Type;

    private void Update()
    {
        if (CutSceneManager.Ins.IsEndCutScene)
        {
            cutsceneHandler.PlayNextElement();
        }

        if (Input.GetButtonDown("Interact"))
        {
            CutSceneManager.Ins.OnResume();
        }
    }

    public override void Execute()
    {
        CutSceneManager.Ins.InitPlayableAsset(data.GetCutSceneByType(cs_Type).asset);
        CutSceneManager.Ins.OnStart();
    }
}
