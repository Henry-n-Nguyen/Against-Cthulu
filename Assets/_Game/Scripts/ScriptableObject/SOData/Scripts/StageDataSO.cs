using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObjects/Data/StageDataSO")]
public class StageDataSO : ScriptableObject
{
    [field: Header("Tutorial")]
    [field: SerializeField] public List<NormalStage> tutorialStages { get; private set;}

    [field: Header("Map 1")]
    [field: SerializeField] public List<NormalStage> stagesMap1 { get; private set;}
    [field: SerializeField] public BossStage bossStageMap1 { get; private set;}

    [field: Header("Map 2")]
    [field: SerializeField] public List<NormalStage> stagesMap2 { get; private set;}
    [field: SerializeField] public BossStage bossStageMap2 { get; private set;}

    [field: Header("Non-combat")]
    [field: SerializeField] public Stage shopStage { get; private set;}
    [field: SerializeField] public Stage hallStage { get; private set;}

    [field: Header("Final Boss")]
    [field: SerializeField] public FinalBossStage finalBossStage { get; private set;}
}
