using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Pause : UICanvas
{
    [SerializeField] private Slider BGMVolume;
    [SerializeField] private Slider SFXVolume;

    private UserData data;

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

    public override void Open()
    {
        base.Open();

        data = UserDataManager.Ins.userData;

        SetAudioSetting();
        PauseGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void SetAudioSetting()
    {
        BGMVolume.value = data.BGMVolume;
        SFXVolume.value = data.SFXVolume;
    }

    public void OnBGMVolumeChanged()
    {
        data.BGMVolume = BGMVolume.value;
        AudioManager.Ins.SetBGMVolume();
    }

    public void OnSFXVolumeChanged()
    {
        data.SFXVolume = SFXVolume.value;
        AudioManager.Ins.SetSFXVolume();
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        GamePlayManager.Ins.OutUI();
        CloseDirectly();
    }

    public void RestartGame()
    {
        GamePlayManager.Ins.OnRestart();
        CloseDirectly();
    }

    public void MainMenu()
    {
        UIManager.Ins.OpenUI<UI_MainMenu>();
        CloseDirectly();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
