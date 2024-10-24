using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObjects/Data/StageDataSO")]
public class StageDataSO : ScriptableObject
{
    [Header("Tutorial")]
    public List<Stage> tutorialStages;

    [Header("Map 1")]
    public List<Stage> stagesMap1;
    public BossStage bossStageMap1;

    [Header("Map 2")]
    public List<Stage> stagesMap2;
    public BossStage bossStageMap2;

    [Header("Non-combat")]
    public Stage shopStage;
    public Stage hallStage;

    [Header("Final Boss")]
    public BossStage finalBossStage;
}
