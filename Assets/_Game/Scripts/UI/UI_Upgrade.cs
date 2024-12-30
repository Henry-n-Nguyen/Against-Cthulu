using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class UI_Upgrade : UICanvas
{
    [SerializeField] private TMP_Text statText;
    [SerializeField] private TMP_Text HPlvText;
    [SerializeField] private TMP_Text DMGlvText;
    [SerializeField] private TMP_Text SPDlvText;
    [SerializeField] private TMP_Text RCDlvText;

    private UserData data;

    // Function
    public override void Open()
    {
        base.Open();

        data = UserDataManager.Ins.userData;

        GamePlayManager.Ins.player.OnHaveUprades += UpdateStat;

        UpdateStat();
        PauseGame();
    }

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        if (Input.GetButtonDown("Escape"))
        {
            ContinueGame();
        }
    }

    public override void CloseDirectly()
    {
        GamePlayManager.Ins.player.OnHaveUprades -= UpdateStat;

        base.CloseDirectly();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        GamePlayManager.Ins.OutUI();
        CloseDirectly();
    }

    // Update Function
    public void UpgradeHP()
    {
        if (data.HP_lv < 10 && data.Diamond >= (data.HP_lv + 1) * 100)
        {
            data.Diamond -= (data.HP_lv + 1) * 100;
            data.HP_lv++;

            HPlvText.text = "HP Lv." + data.HP_lv.ToString() + "\nIncrease 20 HP. Max 200";

            GamePlayManager.Ins.player.OnUpdated();
        }
    }

    public void UpgradeDMG()
    {
        if (data.DMG_lv < 10 && data.Diamond >= (data.DMG_lv + 1) * 120)
        {
            data.Diamond -= (data.DMG_lv + 1) * 120;
            data.DMG_lv++;

            DMGlvText.text = "DMG Lv." + data.DMG_lv.ToString() + "\nIncrease 1 DMG. Max 10";

            GamePlayManager.Ins.player.OnUpdated();
        }
    }

    public void UpgradeSPD()
    {
        if (data.SPD_lv < 10 && data.Diamond >= (data.SPD_lv + 1) * 80)
        {
            data.Diamond -= (data.SPD_lv + 1) * 80;
            data.SPD_lv++;

            SPDlvText.text = "SPD Lv." + data.SPD_lv.ToString() + "\nIncrease 0.1 SPD. Max 1";

            GamePlayManager.Ins.player.OnUpdated();
        }
    }

    public void UpgradeR_CD()
    {
        if (data.R_CD_lv < 10 && data.Diamond >= (data.R_CD_lv + 1) * 150)
        {
            data.Diamond -= (data.R_CD_lv + 1) * 150;
            data.R_CD_lv++;

            RCDlvText.text = "R.CD Lv." + data.R_CD_lv.ToString() + "\nIncrease 0.02 R.CD . Max 0.2";

            GamePlayManager.Ins.player.OnUpdated();
        }
    }

    // private
    private void UpdateStat()
    {
        statText.text = (data.HP).ToString() + "\n"
                        + (data.DMG).ToString() + "\n"
                        + (data.SPD).ToString() + "\n"
                        + (data.R_CD).ToString();
    }
}
