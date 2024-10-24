using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;
using JetBrains.Annotations;

public class StageManager : Singleton<StageManager>
{
    [Header("SO Data")]
    [SerializeField] private StageDataSO data;

    public List<Stage> selectedStage;

    [SerializeField] private Transform holder;

    public Stage currentStage;

    private int index = 0;

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentStage.OnCompleteStage();
        }
    }

    private void OnInit()
    {
        SetUpStage();
        SpawnStage();
    }

    private void SetUpStage()
    {
        int randNum = 0;

        index = 0;

        while (index <= (int)StageIndex.FinalBoss)
        {
            switch (index)
            {
                case (int)StageIndex.Hall:
                    selectedStage.Add(data.hallStage);
                    break;

                case (int)StageIndex.Shop_1:
                case (int)StageIndex.Shop_2:
                    selectedStage.Add(data.shopStage);
                    break;

                case (int)StageIndex.BossFight_Map_1:
                    selectedStage.Add(data.bossStageMap1);
                    break;

                case (int)StageIndex.BossFight_Map_2:
                    selectedStage.Add(data.bossStageMap2);
                    break;

                //case (int)StageIndex.FinalBoss:
                //    selectedStage.Add(data.finalBossStage);
                //    break;

                default:
                    if (index < (int)StageIndex.BossFight_Map_1)
                    {
                        randNum = Random.Range(0, data.stagesMap1.Count);
                        selectedStage.Add(data.stagesMap1[randNum]);
                    }
                    else
                    {
                        randNum = Random.Range(0, data.stagesMap2.Count);
                        selectedStage.Add(data.stagesMap2[randNum]);
                    }
                    break;
            }

            index++;
        }

        index = 0;
    }

    private void SpawnStage()
    {
        if (currentStage != null) 
            Destroy(currentStage.gameObject);

        currentStage = Instantiate(selectedStage[index], holder);

        GamePlayManager.Ins.player.TeleportTo(currentStage.spawnPoint.position);
        GamePlayManager.Ins.confiner.m_BoundingShape2D = currentStage.mapBound;
    }

    public void RestartStage()
    {
        SpawnStage();
    }

    public void NextStage()
    {   
        index++;
        SpawnStage();
    }
}
