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
        Attack = 4,
        Hit = 5,
        Slide = 6,
    }

    public enum PoolType
    {
        None = 0,

        Player = 1,
        Enemy = 2,
        Effect = 3,
        Magic = 4,
    }
}
