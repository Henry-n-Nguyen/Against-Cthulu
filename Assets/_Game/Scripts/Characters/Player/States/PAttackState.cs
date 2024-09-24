using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PAttackState : IState<Player>
{
    private int comboPhase = 1;

    private float timer;
    public void OnEnter(Player t)
    {
        timer = 0f;
        comboPhase = 1;

        t.PreAttack();

        if (!t.IsJumping)
        {
            t.ChangeAnimDirectly(S_Constant.ANIM_ATTACK_FIRST);
            t.SetMove(Vector2.zero + Vector2.up * t.RbVelocity.y);
        }
        else
        {
            t.ChangeAnimDirectly(S_Constant.ANIM_JUMP_UP_ATTACK);
        }
    }

    public void OnExecute(Player t)
    {
        timer += Time.deltaTime;

        if (!t.IsJumping)
        {
            if (timer > 0.5f)
            {
                t.ChangeAnim(S_Constant.ANIM_ATTACK_RECOVER);
            }

            if (timer > 1f)
            {
                t.ChangeState(Player.IDLE_STATE);
            }
        }
        else
        {
            if (timer > 1f)
            {
                if (!t.IsGrounded)
                {
                    t.ChangeState(Player.ON_AIR_STATE);
                }
            }
        }

        GatherAttackInput(t);
    }

    public void OnExit(Player t)
    {
        t.SetBool(CharacterState.Attack, false);
    }

    // Unique function
    private void GatherAttackInput(Player t)
    {
        // Change to idle
        if (t.IsJumping && t.IsGrounded && t.RbVelocity.y < 0.1f)
        {
            t.SetBool(CharacterState.Jump, false);
            t.ChangeAnim(S_Constant.ANIM_JUMP_LANDING);
            t.ChangeState(Player.IDLE_STATE);
        }

        // Trigger another attack
        if (!t.IsJumping && Input.GetButtonDown("Attack") && comboPhase == 1)
        {
            comboPhase = 2;

            t.PreAttack();

            t.ChangeAnimDirectly(S_Constant.ANIM_ATTACK_SECOND);
            t.SetMove(Vector2.zero + Vector2.up * t.RbVelocity.y);
        }
    }
}
