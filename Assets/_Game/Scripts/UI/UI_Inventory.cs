using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : UICanvas
{
    [SerializeField] private TMP_Text statText;

    [Header("Change Page Button")]
    [SerializeField] private GameObject prevPageButton;
    [SerializeField] private GameObject nextPageButton;

    [Header("Magic")]
    [SerializeField] private GameObject magicPage;
    [Space]
    [SerializeField] private GameObject magic_1_Content;
    [SerializeField] private GameObject magic_2_Content;
    [Space]
    [SerializeField] private Image magicIcon_01;
    [SerializeField] private TMP_Text magicInfo_01_Text;
    [SerializeField] private TMP_Text magicStatitics_01_Text;
    [Space]
    [SerializeField] private Image magicIcon_02;
    [SerializeField] private TMP_Text magicInfo_02_Text;
    [SerializeField] private TMP_Text magicStatitics_02_Text;

    [Header("Equipment")]
    [SerializeField] private GameObject equipmentPage;
    [Space]
    [SerializeField] private GameObject equipment_1_Content;
    [SerializeField] private GameObject equipment_2_Content;
    [Space]
    [SerializeField] private Image equipmentIcon_01;
    [SerializeField] private TMP_Text equipmentName_01_Text;
    [SerializeField] private TMP_Text equipmentStat_01_Text;
    [Space]
    [SerializeField] private Image equipmentIcon_02;
    [SerializeField] private TMP_Text equipmentName_02_Text;
    [SerializeField] private TMP_Text equipmentStat_02_Text;


    private UserData data;

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

        UpdateStat();
        UpdateMagic();
        UpdateEquipment();
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

    public void PrevPage()
    {
        prevPageButton.SetActive(false);
        nextPageButton.SetActive(true);

        magicPage.SetActive(false);
        equipmentPage.SetActive(true);
    }

    public void NextPage()
    {
        prevPageButton.SetActive(true);
        nextPageButton.SetActive(false);

        magicPage.SetActive(true);
        equipmentPage.SetActive(false);
    }

    private void UpdateMagic()
    {
        Magic magic_1 = GamePlayManager.Ins.player.magic_1;
        Magic magic_2 = GamePlayManager.Ins.player.magic_2;

        if (magic_1 == null) magic_1_Content.SetActive(false);
        else
        {
            magic_1_Content.SetActive(true);

            magicIcon_01.sprite = magic_1.icon;
            magicInfo_01_Text.text = magic_1.name + "\n" + magic_1.description;
            magicStatitics_01_Text.text = magic_1.CD.ToString() + "\n" + magic_1.Multiplier.ToString();
        }

        if (magic_2 == null) magic_2_Content.SetActive(false);
        else
        {
            magic_2_Content.SetActive(true);

            magicIcon_02.sprite = magic_2.icon;
            magicInfo_02_Text.text = magic_2.name + "\n" + magic_2.description;
            magicStatitics_02_Text.text = magic_2.CD.ToString() + "\n" + magic_2.Multiplier.ToString(); 
        }
    }

    private void UpdateEquipment()
    {
        Equipment weapon = data.Weapon;
        Equipment belt = data.Belt;

        if (weapon == null) equipment_1_Content.SetActive(false);
        else
        {
            equipment_1_Content.SetActive(true);

            equipmentIcon_01.sprite = weapon.Icon;
            equipmentName_01_Text.text = weapon.Name;
            equipmentStat_01_Text.text = weapon.Stat;
        }

        if (belt == null) equipment_2_Content.SetActive(false);
        else
        {
            equipment_2_Content.SetActive(true);

            equipmentIcon_02.sprite = belt.Icon;
            equipmentName_02_Text.text = belt.Name;
            equipmentStat_02_Text.text = belt.Stat;
        }
    }

    private void UpdateStat()
    {
        data = UserDataManager.Ins.userData;

        statText.text = (data.HP).ToString() + "\n"
                        + (data.DMG).ToString() + "\n"
                        + (data.SPD).ToString() + "\n"
                        + (data.R_CD).ToString();
    }
}
