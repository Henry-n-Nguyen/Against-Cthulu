using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using HuySpace;

public class PCastMagicState : IState<Player>
{
    public void OnEnter(Player t)
    {
        if (t.IsJumping)
        {
            t.ChangeAnimDirectly(S_Constant.ANIM_JUMP_CAST_MAGIC);
        }
        else
        {
            t.SetMove(Vector2.zero + Vector2.up * t.RbVelocity.y);
            t.ChangeAnimDirectly(S_Constant.ANIM_CAST_MAGIC);
        }

        t.CastMagic();
    }

    public void OnExecute(Player t)
    {

    }

    public void OnExit(Player t)
    {
        t.SetBool(CharacterState.Attack, false);
    }
}
