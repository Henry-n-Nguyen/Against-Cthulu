using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HuySpace
{
    public enum CharacterState
    {
        None = 0,
        Idle = 1,
        Run = 2,
        Jump = 3,
        DoubleJump = 4,
        Attack = 5,
        Hit = 6,
        Slide = 7,
    }

    public enum PlayerMagicIndex
    {
        First = 0,
        Second = 1,
    }

    public enum CooldownState
    {
        Special_01 = PlayerMagicIndex.First,
        Special_02 = PlayerMagicIndex.Second,
        Slide = 2,
    }

    public enum PoolType
    {
        None = 0,

        Player = 1,

        Hunter_Boss = 10,
        Knight_Boss = 11,
        Rogue_Boss = 12,
        Hashashin_Boss = 13,

        GhostEffect = 40,
        DustEffect = 41,

        FireBall = 50,
        DarkBall = 51,
        IceSpike = 52,

        EarthBump = 60,
        EarthWall = 61,
        DarkZone = 62,
        Arrow_Rain = 63,

        Spear = 70,
        Arrow = 71,
        Boss_Arrow = 72,

        ThunderStrike = 80,

        Laser_Beam = 100,

        Collectable_Coin = 200,
        Collectable_Diamond = 201,

        Normal_BeastMan = 300,
        Normal_CanineBlack = 301,
        Normal_CanineBrown = 302,
        Normal_CanineGray = 303,
        Normal_CanineWhite = 304,
        Normal_Cultist = 305,
        Normal_EvilWizard = 306,
        Normal_EvilDarkWizard = 307,
        Normal_EvilFireWizard = 308,
        Normal_FantasyWarrior = 309,
        Normal_Huntress = 310,
        Normal_Koori = 311,
        Normal_MageSkeleton = 312,
        Normal_WarriorSkeleton = 313,
        Normal_MedievalKing = 314,
        Normal_Mushroom = 315,
        Normal_Slime = 316,
        Normal_StoneGolem_A = 317,
        Normal_StoneGolem_B = 318,
    }

    public enum MagicName
    {
        None = -1,

        FireBall = PoolType.FireBall,
        DarkBall = PoolType.DarkBall,
        IceSpike = PoolType.IceSpike,

        EarthBump = PoolType.EarthBump,
        EarthWall = PoolType.EarthWall,
        DarkZone = PoolType.DarkZone,
        Arrow_Rain = PoolType.Arrow_Rain,

        Spear = PoolType.Spear,
        Arrow = PoolType.Arrow,
        Boss_Arrow = PoolType.Boss_Arrow,

        ThunderStrike = PoolType.ThunderStrike,

        Laser_Beam = PoolType.Laser_Beam,
    }

    public enum TextPosition
    {
        Top,
        Middle,
        Bottom,
    }

    public enum CS_UIType
    {
        FocusCam,
        TransCam,
        Dialogue,
        PsudoPanel,
    }

    public enum MagicDeployType
    {
        Floating = 0,
        InGround = 1,
    }

    public enum StageIndex
    {
        Hall = 0,
        Shop_1 = 4,
        BossFight_Map_1 = 5,
        Shop_2 = 9,
        BossFight_Map_2 = 10,
        FinalBoss = 11,
    }
}
