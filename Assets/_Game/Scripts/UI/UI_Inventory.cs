using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : UICanvas
{
    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        if (Input.GetButtonDown("Escape") || Input.GetButtonDown("Inventory"))
        {
            ContinueGame();
        }
    }

    public override void Open()
    {
        base.Open();

        PauseGame();
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
}
