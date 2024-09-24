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

    public enum CooldownState
    {
        Special_01 = 0,
        Special_02 = 1,
        Slide = 2,
    }

    public enum PoolType
    {
        None = 0,

        Player = 1,

        Enemy = 10,

        GhostEffect = 40,
        DustEffect = 41,

        Magic = 50,
    }

    public enum TextPosition
    {
        Top,
        Middle,
        Bottom,
    }
}
