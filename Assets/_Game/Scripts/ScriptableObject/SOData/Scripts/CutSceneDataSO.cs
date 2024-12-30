using HuySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "SciptableObjects/Data/CutSceneDataSO")]
public class CutSceneDataSO : ScriptableObject
{
    [field: SerializeField] public List<CutSceneAsset> CutScenes { get; private set; }

    public CutSceneAsset GetCutSceneByType(CutSceneType type)
    {
        foreach (CutSceneAsset cutScene in CutScenes)
        {
            if (cutScene.cs_Type == type) return cutScene;
        }

        return null;
    }
}
