using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneManager : Singleton<CutSceneManager>
{
    private PlayableDirector timeline;

    public bool IsEndCutScene { get; private set; }

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    public void InitPlayableAsset(PlayableAsset asset) 
    {
        timeline.playableAsset = asset;
    }

    public void OnStart()
    {
        GamePlayManager.Ins.OnCutScene();
        IsEndCutScene = false;
        timeline.Play();
    }

    public void OnPause()
    {
        timeline.Pause();
    }

    public void OnResume()
    {
        timeline.Resume();
    }

    public void OnStop()
    {
        GamePlayManager.Ins.OutUI();
        IsEndCutScene = true;
        timeline.Stop();
    }
}
