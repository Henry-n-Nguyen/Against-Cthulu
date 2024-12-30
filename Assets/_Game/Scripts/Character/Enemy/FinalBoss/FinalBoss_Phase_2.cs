using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_Phase_2 : Boss_Phase
{
    [SerializeField] private FinalBoss boss;

    private void OnInit()
    {
        boss.IsDeathEvent += OnCompletePhase;
    }
}
