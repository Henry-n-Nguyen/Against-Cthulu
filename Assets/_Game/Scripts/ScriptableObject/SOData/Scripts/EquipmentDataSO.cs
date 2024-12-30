using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObjects/Data/EquipmentDataSO")]
public class EquipmentDataSO : ScriptableObject
{
    [field: Header("Weapon")]
    [field: SerializeField] public List<Equipment> Weapons { get; private set; }


    [field: Header("Belt")]
    [field: SerializeField] public List<Equipment> Belts { get; private set; }

    public Equipment GetRandomWeapon()
    {
        int randNum = Random.Range(0, Weapons.Count);

        return Weapons[randNum];
    }

    public Equipment GetRandomBelt()
    {
        int randNum = Random.Range(0, Belts.Count);

        return Belts[randNum];
    }

    public Equipment GetRandomEquipment()
    {
        int randNum = Random.Range(0, Weapons.Count + Belts.Count);

        return randNum < Weapons.Count ? Weapons[randNum] : Belts[randNum-Weapons.Count];
    }
}
