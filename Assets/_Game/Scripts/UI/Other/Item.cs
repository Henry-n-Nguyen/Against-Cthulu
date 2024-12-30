using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using HuySpace;

public class Item : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private TMP_Text itemPrice;

    [Header("Button")]
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject soldText;

    private Equipment currentItem;

    public void Init(Equipment equipment)
    {
        this.icon.sprite = equipment.Icon;
        this.itemName.text = equipment.Name;
        this.itemDescription.text = equipment.Stat;
        this.itemPrice.text = equipment.Price.ToString();

        currentItem = equipment;
    }

    public void SoldItem()
    {
        UserData data = UserDataManager.Ins.userData;

        if (data.Coin >= currentItem.Price)
        {
            data.Coin -= currentItem.Price;
            
            switch (currentItem.equipmentType)
            {
                case EquipmentType.Weapon:
                    data.Weapon = currentItem;
                    break;
                case EquipmentType.Belt:
                    data.Belt = currentItem;
                    break;
            }

            buyButton.SetActive(false);
            soldText.SetActive(true);

            GamePlayManager.Ins.player.OnUpdated();
        }
    }
}
