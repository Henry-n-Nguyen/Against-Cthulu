using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_Phase_1 : Boss_Phase
{
    private enum MiniBossIndex
    {
        First = 0,
        Second = 1,
        Three = 2,
    }

    private enum BossSkill
    {
        Skill1,
        Skill2,
    }

    [SerializeField] private List<MiniBoss> bossList;


    private List<MiniBossIndex> bossIndexs = new List<MiniBossIndex>();

    private Coroutine triggerSkillCoroutine;
    private Coroutine mixSkillCoroutine;


    private void Start()
    {
        OnInit();
    }

    public override void OnCompletePhase()
    {
        foreach (MiniBoss boss in bossList)
        {
            if (boss.damageable.IsAlive)
            {
                IsCompletePhase = false;
                return;
            }
        }

        IsCompletePhase = true;
    }

    private void OnInit()
    {
        foreach (MiniBoss boss in bossList)
        {
            boss.IsDeathEvent += OnCompletePhase;
        }

        CheckQuantity();

        ProgressSkill(BossSkill.Skill2);
    }

    private void CheckQuantity()
    {
        bossIndexs.Clear();

        if (bossList[0].damageable.IsAlive) bossIndexs.Add(MiniBossIndex.First);
        if (bossList[1].damageable.IsAlive) bossIndexs.Add(MiniBossIndex.Second);
        if (bossList[2].damageable.IsAlive) bossIndexs.Add(MiniBossIndex.Three);

        bossIndexs = ChooseCombo();
    }

    private List<MiniBossIndex> ChooseCombo()
    {
        List<MiniBossIndex> list = new List<MiniBossIndex>();

        int randNum = 0;

        while (bossIndexs.Count > 0)
        {
            if (bossIndexs.Count == 1)
            {
                list.Add(bossIndexs[0]);
                bossIndexs.Clear();
            }
            else
            {
                randNum = Random.Range(0, bossIndexs.Count);

                list.Add(bossIndexs[randNum]);
                bossIndexs.RemoveAt(randNum);
            }
        }

        return list;
    }

    private void ProgressSkill(BossSkill skill)
    {
        switch (skill)
        {
            case BossSkill.Skill1:
                if (!bossList[(int)bossIndexs[0]].damageable.IsAlive) bossIndexs.RemoveAt(0);

                if (bossIndexs.Count > 0)
                {
                    triggerSkillCoroutine = StartCoroutine(TriggerSkill(bossList[(int)bossIndexs[0]], 8f));
                    bossIndexs.RemoveAt(0);
                }
                else
                {
                    CheckQuantity();
                    ProgressSkill(BossSkill.Skill2);
                }

                break;

            case BossSkill.Skill2:
                mixSkillCoroutine = StartCoroutine(MixCombo(3f));
                break;

        }
    }

    private IEnumerator TriggerSkill(MiniBoss boss, float delayTime)
    {
        boss.ChangeAnim("skill_1");

        yield return new WaitForSeconds(delayTime);

        if (bossIndexs.Count == 0)
        {
            CheckQuantity();
            ProgressSkill(BossSkill.Skill2);
        }
        else ProgressSkill(BossSkill.Skill1);
    }

    private IEnumerator MixCombo(float breakTime)
    {
        if (bossList[0].damageable.IsAlive)
        {
            bossList[0].ChangeAnim("skill_2");
            yield return new WaitForSeconds(breakTime);
        }

        if (bossList[1].damageable.IsAlive)
        {
            bossList[1].ChangeAnim("skill_2");
            yield return new WaitForSeconds(breakTime);
        }

        if (bossList[2].damageable.IsAlive)
        {
            bossList[2].ChangeAnim("skill_2");
            yield return new WaitForSeconds(breakTime);
        }

        CheckQuantity();
        ProgressSkill(BossSkill.Skill1);
    }
}
