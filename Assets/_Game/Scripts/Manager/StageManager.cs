using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;
using System;

public class StageManager : Singleton<StageManager>
{
    [Header("SO Data")]
    [SerializeField] private StageDataSO data;

    public List<NormalStage> selectedStage;

    [SerializeField] private Transform holder;

    private List<NormalStage> stagesTutorial = new List<NormalStage>();
    private List<NormalStage> stagesChapter1 = new List<NormalStage>();
    private List<NormalStage> stagesChapter2 = new List<NormalStage>();

    public event Action OnEndNormalStage;
    public event Action OnNewNormalStage;

    public Stage currentStage { get; private set; }

    private int index = 0;
    private int tutorialIndex = 0;

    private bool isOnTutorial;
    private bool isOnHallStage;
    private bool isChapter1;
    private bool isChapter2;
    private bool isOnShop;
    private bool isOnBossChapter;
    private bool isOnFinalBoss;

    public void OnInit()
    {
        foreach (Transform child in holder.GetComponentInChildren<Transform>())
        {
            if (child != holder)
            {
                Destroy(child.gameObject);
            }
        }

        selectedStage.Clear();

        isOnTutorial = !UserDataManager.Ins.userData.passTutorial;
        isOnHallStage = true;
        isChapter1 = false;
        isChapter2 = false;
        isOnShop = false;
        isOnBossChapter = false;
        isOnFinalBoss = false;

        SetUpStage();

        if (isOnTutorial) currentStage = Instantiate(data.tutorialStages[0], holder);
        else currentStage = Instantiate(data.hallStage, holder);

        SpawnStage();
    }

    private void SetUpStage()
    {
        int randNum = 0;

        if (isOnTutorial)
        {
            tutorialIndex = 0;
            stagesTutorial.Clear();
            stagesTutorial.AddRange(data.tutorialStages);
        }

        index = 0;
        
        stagesChapter1.Clear();
        stagesChapter2.Clear();

        List<NormalStage> chap1 = new List<NormalStage>();
        chap1.AddRange(data.stagesMap1);

        List<NormalStage> chap2 = new List<NormalStage>();
        chap2.AddRange(data.stagesMap2);

        int gate = 1;

        while (chap1.Count > 0)
        {
            randNum = UnityEngine.Random.Range(0, chap1.Count);

            NormalStage stage = chap1[randNum];

            if (gate == 1)
            {
                if (UnityEngine.Random.Range(0,3) == 1 || (stagesChapter1.Count == chap1.Count - 1 && gate == 1))
                {
                    stage.IsEndChapter = true;
                    gate--;
                }
                else
                {
                    stage.IsEndChapter = false;
                }
            }
            else
            {
                stage.IsEndChapter = false;
            }

            stagesChapter1.Add(stage);
            chap1.RemoveAt(randNum);
        }

        gate = 1;

        while (chap2.Count > 0)
        {
            randNum = UnityEngine.Random.Range(0, chap2.Count);

            NormalStage stage = chap2[randNum];

            if (gate == 1)
            {
                if (UnityEngine.Random.Range(0, 3) == 1 || (stagesChapter2.Count == chap2.Count - 1 && gate == 1))
                {
                    stage.IsEndChapter = true;
                    gate--;
                }
                else
                {
                    stage.IsEndChapter = false;
                }
            }
            else
            {
                stage.IsEndChapter = false;
            }

            stagesChapter2.Add(stage);
            chap2.RemoveAt(randNum);
        }
    }

    private void SpawnStage()
    {
        GamePlayManager.Ins.player.TeleportTo(currentStage.spawnPoint.position);
        GamePlayManager.Ins.confiner.m_BoundingShape2D = currentStage.mapBound;
    }

    public void RestartStage()
    {
        SpawnStage();
    }

    public void NextStage()
    {
        if (!isChapter1 && !isChapter2) return;

        currentStage.gameObject.SetActive(false);

        index = (index + 1) % 6;

        if (isChapter1)
        {
            if (index + 1 > selectedStage.Count)
            {
                selectedStage.Add(Instantiate(stagesChapter1[index], holder));
                currentStage = selectedStage[index];
            }
            else
            {
                currentStage = selectedStage[index];
                currentStage.gameObject.SetActive(true);
            }
        }
        
        if (isChapter2)
        {
            if (index + 1 > selectedStage.Count)
            {
                selectedStage.Add(Instantiate(stagesChapter2[index], holder));
                currentStage = selectedStage[index];
            }
            else
            {
                currentStage = selectedStage[index];
                currentStage.gameObject.SetActive(true);
            }
        }

        if (currentStage.IsEndStage) OnNewNormalStage?.Invoke();

        SpawnStage();
    }

    public void NextTutorial()
    {
        if (tutorialIndex == stagesTutorial.Count - 1)
        {
            return;
        }

        if (currentStage != null) Destroy(currentStage.gameObject);

        tutorialIndex++;

        currentStage = Instantiate(stagesTutorial[tutorialIndex], holder);

        SpawnStage();

        if (tutorialIndex == stagesTutorial.Count - 1)
        {
            isOnTutorial = false;
            UserDataManager.Ins.userData.passTutorial = true;
        }
    }

    public void EndChapter()
    {
        if (currentStage != null) Destroy(currentStage.gameObject);

        if (isOnTutorial)
        {
            NextTutorial();
            return;
        }

        if (isOnHallStage)
        {
            isOnHallStage = false;
            isChapter1 = true;

            index = -1;
            NextStage();
            return;
        }
        else
        {
            if (selectedStage.Count > 0)
            {
                foreach (Stage stage in selectedStage)
                {
                    Destroy(stage.gameObject);
                }

                selectedStage.Clear();
            }

            if (!isOnShop)
            {
                isOnShop = true;

                currentStage = Instantiate(data.shopStage, holder);
            }
            else
            {
                if (!isOnFinalBoss)
                {
                    if (!isOnBossChapter)
                    {
                        isOnBossChapter = true;

                        if (isChapter1) currentStage = Instantiate(data.bossStageMap1, holder);
                        else if (isChapter2) currentStage = Instantiate(data.bossStageMap2, holder);
                    }
                    else
                    {
                        if (isChapter1)
                        {
                            isChapter1 = false;
                            isChapter2 = true;
                            isOnBossChapter = false;

                            index = -1;
                            NextStage();
                            return;
                        }

                        if (isChapter2)
                        {
                            isChapter2 = false;
                            isOnFinalBoss = true;

                            currentStage = Instantiate(data.shopStage, holder);
                        }
                    }
                }
                else
                {
                    currentStage = Instantiate(data.finalBossStage, holder);
                }
            }
        }

        SpawnStage();
    }

    public void EndNormalStage()
    {
        OnEndNormalStage?.Invoke();
    }
}
