using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStage : Stage
{
    [Header("SO Datas")]
    [SerializeField] private BossDataSO data;

    [Header("References")]
    [SerializeField] private BossFight_SubStage subStage;
    [SerializeField] private Transform bossSpawnPoint;

    [HideInInspector] public BossEnemy bossEnemy;

    private void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        bossEnemy = ChooseBoss();
        bossEnemy.IsDeathEvent += OnCompleteStage;
        bossEnemy.IsDeathEvent += subStage.OnComplete;
    }

    private void OnDespawn()
    {
        bossEnemy.IsDeathEvent -= OnCompleteStage;
        bossEnemy.IsDeathEvent -= subStage.OnComplete;
    }

    private BossEnemy ChooseBoss()
    {
        int randNum = Random.Range(0, data.bossEnemies.Count);

        return SimplePool.Spawn<BossEnemy>(data.bossEnemies[randNum].poolType, bossSpawnPoint);
    }
}
