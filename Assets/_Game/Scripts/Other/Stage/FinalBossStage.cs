using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossStage : Stage
{
    [SerializeField] private List<Boss_Phase> bossPhases;

    private Boss_Phase currentBossPhase;
    private int bossPhaseIndex = 0;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (currentBossPhase.IsCompletePhase)
        {
            OnNextBossPhase();
        }
    }

    private void OnInit()
    {
        bossPhaseIndex = 0;
        currentBossPhase = bossPhases[bossPhaseIndex];
        currentBossPhase.gameObject.SetActive(true);
    }

    private void OnNextBossPhase()
    {
        if (bossPhaseIndex == bossPhases.Count) OnCompleteStage();
        else
        {
            currentBossPhase.gameObject.SetActive(false);
            bossPhaseIndex++;
            currentBossPhase = bossPhases[bossPhaseIndex];
            currentBossPhase.gameObject.SetActive(true);
        }
    }
}
