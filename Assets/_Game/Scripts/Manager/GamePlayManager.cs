using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.Playables;

public class GamePlayManager : Singleton<GamePlayManager>
{
    [Header("Public Character")]
    public Player player;
    
    [Header("CutScene Timeline")]
    public PlayableDirector timeline;

    [Header("References")]
    public Camera cam;
    public CinemachineVirtualCamera vCam;
    public CinemachineConfiner2D confiner;

    [Header("UI")]
    public GameObject uiCanvas;

    [Header("Currency")]
    public int coinEarned;
    public int diamondEarned;

    public bool CanInteract 
    { 
        get
        {
            return !isInUI;
        }
        private set 
        {
            return;        
        }
    }

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
            OnPause();
        }
        else if (Input.GetButtonDown("Inventory"))
        {
            OnInventory();
        }
    }

    private void Start()
    {
        OnStart();
    }

    public void OnInit()
    {
        ResetTemporaryStat();
        StageManager.Ins.OnInit();
    }

    private void ResetTemporaryStat()
    {
        UserData userData = UserDataManager.Ins.userData;

        coinEarned = 0;
        diamondEarned = 0;

        userData.Weapon = null;
        userData.Belt = null;
        userData.Skill_1 = null;
        userData.Skill_2 = null;

        userData.Coin = 0;
    }

    public void OnStart()
    {
        isInUI = true;
        UIManager.Ins.OpenUI<UI_MainMenu>();
    }

    public void OnRestart()
    {
        StageManager.Ins.OnInit();
        OnPlay();
    }

    public void OnUpgrade()
    {
        isInUI = true;
        UIManager.Ins.OpenUI<UI_Upgrade>();
    }

    public void OnShop()
    {
        isInUI = true;
        UIManager.Ins.OpenUI<UI_Shop>();
    }

    public void OnPlay()
    {
        player.OnInit();
        OnInit();
        UIManager.Ins.OpenUI<UI_InGame>();
    }

    public void OnPause()
    {
        isInUI = true;
        UIManager.Ins.OpenUI<UI_Pause>();
    }

    public void OnInventory()
    {
        isInUI = true;
        UIManager.Ins.OpenUI<UI_Inventory>();
    }

    public void OnTutorial()
    {

    }

    public IEnumerator OnResult()
    {
        isInUI = true;

        yield return new WaitForSeconds(2f);

        UIManager.Ins.OpenUI<UI_Result>();
    }

    public void OnCutScene()
    {
        isInUI = true;
        uiCanvas.SetActive(false);
    }

    public void OnUI()
    {
        isInUI = true;
    }

    public void OutUI()
    {
        isInUI = false;
        uiCanvas.SetActive(true);
    }

    public void CoinUpdated(int coin)
    {
        coinEarned += coin;
        OnCoinChanged?.Invoke();
    }

    public void DiamondUpdated(int diamond)
    {
        diamondEarned += diamond;
        OnDiamondChanged?.Invoke();
    }
}
