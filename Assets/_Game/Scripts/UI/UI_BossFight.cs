using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_BossFight : UICanvas
{
    [SerializeField] private RectTransform hpPanel;
    [SerializeField] private Image portrait;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text currentHpText;

    private BossStage bossStage;

    private float hpLength;

    public override void Open()
    {
        base.Open();

        hpLength = hpPanel.sizeDelta.x;

        bossStage = StageManager.Ins.currentStage.GetComponent<BossStage>();

        bossStage.bossEnemy.IsHitOrHealEvent += UpdateCurrentHealth;

        portrait.sprite = bossStage.bossEnemy.Portrait;
        nameText.text = bossStage.bossEnemy.Name;
        currentHpText.text = bossStage.bossEnemy.damageable.HP.ToString();

        destroyOnClose = true;
    }

    public override void CloseDirectly()
    {
        bossStage.bossEnemy.IsHitOrHealEvent -= UpdateCurrentHealth;

        base.CloseDirectly();
    }

    private void UpdateCurrentHealth()
    {
        currentHpText.text = bossStage.bossEnemy.damageable.HP.ToString();
        hpPanel.sizeDelta = new Vector2(hpLength * (bossStage.bossEnemy.damageable.HP / bossStage.bossEnemy.damageable.MaxHP), 0);
    }
}
