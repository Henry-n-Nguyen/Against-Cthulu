using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutSceneHandler : MonoBehaviour
{
    [Header("Check CutScene")]
    public bool isOneTrigger;
    public bool freePlayer;
    public bool isDetectedPlayer;

    [Header("References")]
    public Camera cam;
    public CinemachineVirtualCamera vCam;

    [SerializeField] private CutSceneElementBase[] cutSceneElementBases;

    private int index = -1;

    private void Start()
    {
        OnInit();

        cutSceneElementBases = GetComponents<CutSceneElementBase>();
    }

    public void OnInit()
    {
        index = -1;
    }

    public void ExecuteCurrentElement() 
    {
        if (index >= 0 && index < cutSceneElementBases.Length)
        {
            GamePlayManager.Ins.player.Freeze(freePlayer);
            cutSceneElementBases[index].Execute();
        }
    }

    public void PlayNextElement() 
    {
        index++;
        ExecuteCurrentElement();
    }

    public void ReleaseAllElement()
    {
        if (!isOneTrigger) OnInit();

        for (int i = 0; i < cutSceneElementBases.Length; i++)
        {
            cutSceneElementBases[i].Release();
        }

        GamePlayManager.Ins.player.Freeze(false);
    }
}
