using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class Equipment : MonoBehaviour
{
    [field: Header("Info")]
    [field: SerializeField] public EquipmentType equipmentType { get; private set; }
    [field: Space]
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public string Stat 
    { 
        get
        {
            return (HP == 0 ? "" : "HP + " + (HP * 20).ToString() + "\n")
                 + (DMG == 0 ? "" : "DMG + " + DMG.ToString() + "\n")
                 + (SPD == 0 ? "" : "SPD + " + (SPD * 0.1f).ToString() + "\n")
                 + (R_CD == 0 ? "" : "R.CD + " + (R_CD * 0.1).ToString() + "\n");
        } 
        private set
        {
            Stat = value;
        }
    }
    [field: SerializeField] public int Price { get; private set; }


    [field: Header("Stats")]
    [field: SerializeField] public int HP { get; private set; }
    [field: SerializeField] public int DMG { get; private set; }
    [field: SerializeField] public int SPD { get; private set; }
    [field: SerializeField] public int R_CD { get; private set; }
}
