using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class GamePlayManager : Singleton<GamePlayManager>
{
    [Header("Public Character")]
    public Player player;

    [Header("References")]
    public Camera cam;
    public CinemachineVirtualCamera vCam;
    public CinemachineConfiner2D confiner;

    // Action
    public event Action OnCoinChanged;
    public event Action OnDiamondChanged;

    private bool isInUI;

    private void Update()
    {
        if (isInUI)
        {
            return;
        }

        if (Input.GetButtonDown("Escape"))
        {
            isInUI = true;
            UIManager.Ins.OpenUI<UI_Pause>();
        }

        if (Input.GetButtonDown("Inventory"))
        {
            isInUI = true;
            UIManager.Ins.OpenUI<UI_Inventory>();
        }
    }

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        isInUI = true;
        UIManager.Ins.OpenUI<UI_MainMenu>();
    }

    public void OnPlay()
    {
        UIManager.Ins.OpenUI<UI_InGame>();
    }

    public void OnTutorial()
    {

    }

    public void OutUI()
    {
        isInUI = false;
    }

    public void CoinUpdated()
    {
        OnCoinChanged?.Invoke();
    }

    public void DiamondUpdated()
    {
        OnDiamondChanged?.Invoke();
    }
}
