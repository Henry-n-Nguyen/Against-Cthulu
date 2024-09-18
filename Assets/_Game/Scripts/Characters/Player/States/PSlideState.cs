using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PSlideState : IState<Player>
{
    public void OnEnter(Player t)
    {
        if (t.IsJumping)
        {
            t.ChangeAnimDirectly(S_Constant.ANIM_JUMP_SLIDE);
        }
        else
        {
            t.ChangeAnimDirectly(S_Constant.ANIM_SLIDE);
        }
    }

    public void OnExecute(Player t)
    {
        t.SetMove(Vector2.right * t.RbVelocity.x);
    }

    public void OnExit(Player t)
    {
        t.SetBool(CharacterState.Slide, false);
    }
}
