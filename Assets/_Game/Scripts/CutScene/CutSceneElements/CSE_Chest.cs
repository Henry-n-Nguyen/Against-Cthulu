using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class CSE_Chest : CutSceneElementBase
{
    [Header("Data")]
    [SerializeField] private MagicDataSO magicData;

    [Header("References")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private Animator anim;

    [Header("Item")]
    [SerializeField] private GameObject item;
    [SerializeField] private SpriteRenderer itemIcon;
    [SerializeField] private GameObject itemIndicator;

    private bool started = false;
    private bool itemCollected = false;
    private Magic magic;

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        if (!cutsceneHandler.isDetectedPlayer)
        {
            return;
        }

        if (!started && Input.GetButtonDown("Interact"))
        {
            OpenChest();
        }

        if (started && !itemCollected && Input.GetButtonDown("Swap1"))
        {
            CollectItem(1);
        }
        else if (started && !itemCollected && Input.GetButtonDown("Swap2"))
        {
            if (UserDataManager.Ins.userData.special_1 == MagicName.None) CollectItem(1);
            else CollectItem(2);
        }
    }

    public override void Execute()
    {
        if (!started) ToggleIndicator(true);
        else
        {
            if (!itemCollected) ToggleItemIndicator(true);
        }
    }

    public override void Release()
    {
        ToggleIndicator(false);
        ToggleItemIndicator(false);
    }

    private void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    private void ToggleItemIndicator(bool show)
    {
        itemIndicator.SetActive(show);
    }

    private void OpenChest()
    {
        if (started)
        {
            return;
        }

        started = true;
        ToggleIndicator(false);

        GetItem();
    }

    private void GetItem()
    {
        if (magic != null)
        {
            return;
        }

        // Random Item or Magic
        magic = magicData.GetRandomMagic();

        itemIcon.sprite = magic.GetMagicSprite();

        switch (magic.DeployType)
        {
            case MagicDeployType.Floating:
                itemIcon.transform.localPosition = Vector2.zero;
                break;
            case MagicDeployType.InGround:
                itemIcon.transform.localPosition = Vector2.zero + Vector2.up * -0.25f;
                break;
        }

        ShowItem();
    }

    private void ShowItem()
    {
        // Show item
        ToggleItemIndicator(true);
        anim.Play("Open");
    }

    private void CollectItem(int index)
    {
        UserData data = UserDataManager.Ins.userData;

        anim.Play("Despawn");
        itemCollected = true;

        switch (index)
        {
            case 1: 
                data.special_1 = (MagicName)magic.poolType;
                break;
            case 2:
                data.special_2 = (MagicName)magic.poolType;
                break;
        }

        GamePlayManager.Ins.player.OnSwapSkill();
    }
}
