using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CutSceneHandler : MonoBehaviour
{
    [Header("Check CutScene")]
    public bool isOneTrigger;

    [Header("References")]
    public Camera cam;
    public CinemachineVirtualCamera vCam;

    [SerializeField] private CutSceneElementBase[] cutSceneElementBases;

    private int index = -1;

    private void Start()
    {
        cutSceneElementBases = GetComponents<CutSceneElementBase>();
    }

    public void ExecuteCurrentElement() 
    {
        if (index >= 0 && index < cutSceneElementBases.Length)
        {
            cutSceneElementBases[index].Excecute();
        }
    }

    public void PlayNextElement() 
    {
        index++;
        ExecuteCurrentElement();
    }

    public void ReleaseAllElement()
    {
        index = -1;

        for (int i = 0; i < cutSceneElementBases.Length; i++)
        {
            cutSceneElementBases[i].Release();
        }
    }
}
