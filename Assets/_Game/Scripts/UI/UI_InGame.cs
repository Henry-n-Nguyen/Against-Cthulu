using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using HuySpace;

public class UI_InGame : UICanvas
{
    [SerializeField] private RectTransform hpPanel;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text max_HPText;

    [SerializeField] private TMP_Text diamondText;
    [SerializeField] private TMP_Text coinText;

    [SerializeField] private Image skill_1_Icon;
    [SerializeField] private Image skill_1_CD_frame;
    [SerializeField] private Image skill_2_Icon;
    [SerializeField] private Image skill_2_CD_frame;

    [SerializeField] private GameObject noticeText;

    private float hpLength;

    private UserData data;

    // Call before active Canvas
    public override void Open()
    {
        hpLength = hpPanel.sizeDelta.x;

        data = UserDataManager.Ins.userData;

        GamePlayManager.Ins.player.OnHaveUprades += UpdateMaxHealth;
        GamePlayManager.Ins.player.OnHaveUprades += UpdateCurrentHealth;
        GamePlayManager.Ins.player.IsHitOrHealEvent += UpdateCurrentHealth;
        GamePlayManager.Ins.player.OnSkill_01_CooldownEvent += TriggerSkill_01_CD;
        GamePlayManager.Ins.player.OnSkill_02_CooldownEvent += TriggerSkill_02_CD;
        GamePlayManager.Ins.player.OnSkill_Swap += UpdateSkillIcon;
        GamePlayManager.Ins.OnCoinChanged += UpdateCoinText;
        GamePlayManager.Ins.OnDiamondChanged += UpdateDiamondText;

        StageManager.Ins.OnEndNormalStage += DisplayNotice;
        StageManager.Ins.OnNewNormalStage += NonDisplayNotice;

        UpdateSkillIcon();
        UpdateMaxHealth();
        UpdateCurrentHealth();
        UpdateCoinText();
        UpdateDiamondText();

        base.Open();
    }

    public override void CloseDirectly()
    {
        GamePlayManager.Ins.player.OnHaveUprades -= UpdateMaxHealth;
        GamePlayManager.Ins.player.OnHaveUprades -= UpdateCurrentHealth;
        GamePlayManager.Ins.player.IsHitOrHealEvent -= UpdateCurrentHealth;
        GamePlayManager.Ins.player.OnSkill_01_CooldownEvent -= TriggerSkill_01_CD;
        GamePlayManager.Ins.player.OnSkill_02_CooldownEvent -= TriggerSkill_02_CD;
        GamePlayManager.Ins.player.OnSkill_Swap -= UpdateSkillIcon;
        GamePlayManager.Ins.OnCoinChanged -= UpdateCoinText;
        GamePlayManager.Ins.OnDiamondChanged -= UpdateDiamondText;

        StageManager.Ins.OnEndNormalStage -= DisplayNotice;
        StageManager.Ins.OnNewNormalStage -= NonDisplayNotice;

        base.CloseDirectly();
    }

    //Canvas Function
    private void UpdateMaxHealth()
    {
        max_HPText.text = GamePlayManager.Ins.player.damageable.MaxHP.ToString();
        hpPanel.sizeDelta = new Vector2(hpLength * (GamePlayManager.Ins.player.damageable.HP / GamePlayManager.Ins.player.damageable.MaxHP), 0);
    }

    private void UpdateCurrentHealth()
    {
        hpText.text = GamePlayManager.Ins.player.damageable.HP.ToString();
        hpPanel.sizeDelta = new Vector2(hpLength * (GamePlayManager.Ins.player.damageable.HP / GamePlayManager.Ins.player.damageable.MaxHP), 0);
    }

    private void UpdateCoinText()
    {
        coinText.text = data.Coin.ToString();
    }

    private void UpdateDiamondText()
    {
        diamondText.text = data.Diamond.ToString();
    }

    private void UpdateSkillIcon()
    {
        UserData data = UserDataManager.Ins.userData;

        if (data.Skill_1 != null) 
        {
            skill_1_Icon.gameObject.SetActive(true);
            skill_1_Icon.sprite = GamePlayManager.Ins.player.magic_1.icon;
        }

        if (data.Skill_2 != null)
        {
            skill_2_Icon.gameObject.SetActive(true);
            skill_2_Icon.sprite = GamePlayManager.Ins.player.magic_2.icon;
        }
    }

    private void TriggerSkill_01_CD()
    {
        StartCoroutine(StartSkill_01_CD(GamePlayManager.Ins.player.currentMagic.CD));
    }

    private IEnumerator StartSkill_01_CD(float time)
    {
        skill_1_CD_frame.gameObject.SetActive(true);

        float currentTime = time;

        while (skill_1_CD_frame.fillAmount > 0)
        {
            currentTime -= Time.deltaTime;
            skill_1_CD_frame.fillAmount = currentTime / time;

            yield return null;
        }

        skill_1_CD_frame.gameObject.SetActive(false);
        skill_1_CD_frame.fillAmount = 1;
    }

    private void TriggerSkill_02_CD()
    {
        StartCoroutine(StartSkill_02_CD(GamePlayManager.Ins.player.currentMagic.CD));
    }

    private IEnumerator StartSkill_02_CD(float time)
    {
        skill_2_CD_frame.gameObject.SetActive(true);

        float currentTime = time;

        while (skill_2_CD_frame.fillAmount > 0)
        {
            currentTime -= Time.deltaTime;
            skill_2_CD_frame.fillAmount = currentTime / time;

            yield return null;
        }

        skill_2_CD_frame.gameObject.SetActive(false);
        skill_2_CD_frame.fillAmount = 1;
    }

    private void DisplayNotice()
    {
        noticeText.SetActive(true);
    }

    private void NonDisplayNotice()
    {
        noticeText.SetActive(false);
    }

    public void PauseGame()
    {
        GamePlayManager.Ins.OnPause();
    }
}
