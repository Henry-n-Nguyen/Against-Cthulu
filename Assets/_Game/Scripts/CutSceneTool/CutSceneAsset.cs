using HuySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneAsset : MonoBehaviour
{
    [field: SerializeField] public CutSceneType cs_Type;
    [field: SerializeField] public PlayableAsset asset { get; private set; }
}
