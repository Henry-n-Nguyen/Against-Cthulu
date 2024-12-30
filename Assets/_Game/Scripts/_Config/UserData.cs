using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

[Serializable]
public class UserData
{
    public bool passTutorial;

    public int HP
    {
        get
        {
            return HP_base + HP_lv * 20 + HP_equipment;
        }
        private set
        {
            HP = value;
        }
    }

    public int DMG
    {
        get
        {
            return DMG_base + DMG_lv + DMG_equipment;
        }
        private set
        {
            DMG = value;
        }
    }

    public float SPD
    {
        get
        {
            return SPD_base + SPD_lv * 0.1f + SPD_equipment;
        }
        private set
        {
            SPD = value;
        }
    }
    public float R_CD
    {
        get
        {
            return R_CD_base + R_CD_lv * 0.02f + R_CD_equipment;
        }
        private set
        {
            R_CD = value;
        }
    }

    [Header("Base Stats")]
    public int HP_base = 100;
    public int DMG_base = 5;
    public float SPD_base = 4;
    public float R_CD_base = 0;

    [Header("Update")]
    public int HP_lv = 0;
    public int DMG_lv = 0;
    public int SPD_lv = 0;
    public int R_CD_lv = 0;

    [Header("Equipment Stat")]
    public Equipment Weapon;
    public Equipment Belt;

    [SerializeField] public int HP_equipment
    {
        get
        {
            return (Weapon == null ? 0 : Weapon.HP) + (Belt == null ? 0 : Belt.HP);
        }
        private set
        {
            HP_equipment = value;
        }
    }
    [SerializeField] public int DMG_equipment
    {
        get
        {
            return (Weapon == null ? 0 : Weapon.DMG) + (Belt == null ? 0 : Belt.DMG);
        }
        private set
        {
            DMG_equipment = value;
        }
    }
    [SerializeField] public int SPD_equipment
    {
        get
        {
            return (Weapon == null ? 0 : Weapon.SPD) + (Belt == null ? 0 : Belt.SPD);
        }
        private set
        {
            SPD_equipment = value;
        }
    }
    [SerializeField] public int R_CD_equipment
    {
        get
        {
            return (Weapon == null ? 0 : Weapon.R_CD) + (Belt == null ? 0 : Belt.R_CD);
        }
        set
        {
            R_CD_equipment = value;
        }
    }

    [Header("Skill")]
    public Magic Skill_1;
    public Magic Skill_2;

    [Header("Currency")]
    public int Coin = 0;
    public int Diamond = 0;

    [Header("Settings")]
    public float BGMVolume = 1f;
    public float SFXVolume = 1f;
}
