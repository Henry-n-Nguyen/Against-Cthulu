using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : UICanvas
{
    [Header("Data")]
    [SerializeField] private EquipmentDataSO data;

    [SerializeField] private Transform itemHolder;
    [SerializeField] private Item itemPrefab;

    private bool isSetUp = false;

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

        destroyOnClose = true;

        SetUpShop();
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
        Hide();
    }

    private void SetUpShop()
    {
        if (isSetUp) return;

        Item item;

        int quantityOfItem = 3;

        for (int i = 0; i < quantityOfItem; i++)
        {
            if (i == 0)
            {
                item = Instantiate(itemPrefab, itemHolder);
                Equipment equipment = data.GetRandomWeapon();
                item.Init(equipment);
            }

            if (i == 1)
            {
                item = Instantiate(itemPrefab, itemHolder);
                Equipment equipment = data.GetRandomBelt();
                item.Init(equipment);
            }

            if (i >= 2)
            {
                item = Instantiate(itemPrefab, itemHolder);
                Equipment equipment = data.GetRandomEquipment();
                item.Init(equipment);
            }
        }
        
        isSetUp = true;
    }
}
