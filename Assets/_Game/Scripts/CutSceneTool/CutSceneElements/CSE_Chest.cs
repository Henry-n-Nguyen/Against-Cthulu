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

    private SpriteRenderer spriteRenderer;
    private bool isActivated = false;
    private bool started = false;
    private bool itemCollected = false;
    private Magic magic;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        isActivated = false;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.25f);
    }

    private void Update()
    {
        GatherInput();
    }

    private void GatherInput()
    {
        if (StageManager.Ins.currentStage.IsEndStage) Activate();
        else return;

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
            if (UserDataManager.Ins.userData.Skill_1 == null) CollectItem(1);
            else CollectItem(2);
        }
    }

    public override void Execute()
    {
        isActivated = false;

        if (!started && StageManager.Ins.currentStage.IsEndStage)
        {
            ToggleIndicator(true);
        }
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

        AudioManager.Ins.PlaySFX(SFX.Chest_Open);

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

        itemIcon.sprite = magic.icon;

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
                data.Skill_1 = magic;
                break;
            case 2:
                data.Skill_2 = magic;
                break;
        }

        GamePlayManager.Ins.player.OnSwapSkill();
    }

    private void Activate()
    {
        isActivated = true;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
    }
}
