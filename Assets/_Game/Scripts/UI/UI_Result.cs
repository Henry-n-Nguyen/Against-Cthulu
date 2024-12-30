using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Result : UICanvas
{
    [Header("References")]
    [SerializeField] private GameObject winPart;
    [SerializeField] private GameObject losePart;
    [Space]
    [SerializeField] private TMP_Text coinEarnedText;
    [SerializeField] private TMP_Text diamondEarnedText;

    public override void Open()
    {
        base.Open();

        destroyOnClose = true;

        CheckWinLoseState();
        OnCurrencyChange();
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        GamePlayManager.Ins.OnPlay();
        GamePlayManager.Ins.OutUI();
        CloseDirectly();
    }

    private void CheckWinLoseState()
    {
        Player player = GamePlayManager.Ins.player;

        if (player.damageable.IsAlive)
        {
            winPart.SetActive(true);
            losePart.SetActive(false);
        }
        else
        {
            winPart.SetActive(false);
            losePart.SetActive(true);
        }
    }

    private void OnCurrencyChange()
    {
        coinEarnedText.text = GamePlayManager.Ins.coinEarned.ToString();
        diamondEarnedText.text = GamePlayManager.Ins.diamondEarned.ToString();
    }
}
