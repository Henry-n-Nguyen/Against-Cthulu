using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class NormalStage : Stage
{
    [Header("SO Datas")]
    [SerializeField] private NormalEnemyDataSO data;

    [Header("References")]
    [SerializeField] private Transform zone_1;
    [SerializeField] private Transform zone_2;
    [SerializeField] private GameObject gate;
    private bool isFinalPhase = false;
    private bool isNotice = false;

    private List<NormalEnemy> currentEnemies;

    public bool isTutorial = false;
    public bool IsEndChapter;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (IsEndChapter == true && isNotice)
        {
            isNotice = true;
            StageManager.Ins.EndNormalStage();
        }

        GatherInput();
    }

    private void GatherInput()
    {
        if (IsEndStage && Input.GetButtonUp("ChangeStage"))
        {
            if (!isTutorial) StageManager.Ins.NextStage();
            else StageManager.Ins.NextTutorial();
        }
    }

    private void CheckEnemyPhase()
    {
        int i = 0;

        foreach (NormalEnemy enemy in currentEnemies)
        {
            if (!enemy.damageable.IsAlive) i++;
        }

        if (i < currentEnemies.Count) return;

        if (!isFinalPhase)
        {
            isFinalPhase = true;

            foreach (NormalEnemy enemy in currentEnemies)
            {
                enemy.OnDeath -= CheckEnemyPhase;
            }

            currentEnemies = ChooseEnemies(5, zone_2);
        }
        else OnCompleteStage();
    }

    private void OnInit()
    {
        isFinalPhase = false;

        if (IsEndChapter) gate.SetActive(true);

        if (!isTutorial) currentEnemies = ChooseEnemies(5, zone_1);
    }

    private List<NormalEnemy> ChooseEnemies(int quantity, Transform spawnPoint)
    {
        List<NormalEnemy> enemies = new List<NormalEnemy>();

        for (int i = 0; i < quantity; i++)
        {
            NormalEnemy enemy = SimplePool.Spawn<NormalEnemy>(data.GetRandomEnemies().poolType, spawnPoint);

            enemy.OnDeath += CheckEnemyPhase;

            enemies.Add(enemy);
        }

        return enemies;
    }
}
