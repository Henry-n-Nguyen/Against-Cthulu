using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PSlideState : IState<Player>
{
    private float timer;

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
        timer += Time.deltaTime;

        t.SetMove(Vector2.right * t.RbVelocity.x);
        
        if (timer > 0.1f)
        {
            timer = 0f;
            t.SpawnGhostEffect();
            if (t.IsGrounded) t.SpawnDustEffect();
        }
    }

    public void OnExit(Player t)
    {
        t.SetBool(CharacterState.Slide, false);
    }
}
