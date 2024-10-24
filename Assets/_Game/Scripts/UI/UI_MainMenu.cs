using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainMenu : UICanvas
{
    public override void Open()
    {
        base.Open();

        destroyOnClose = true;

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
}
