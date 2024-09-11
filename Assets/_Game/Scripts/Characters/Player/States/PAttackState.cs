using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PAttackState : IState<AbstractCharacter>
{
    private int comboPhase = 1;

    private float timer;
    public void OnEnter(AbstractCharacter t)
    {
        timer = 0f;
        comboPhase = 1;

        if (t.IsGrounded && !t.IsJumping)
        {
            t.Flip();
            t.ChangeAnim(S_Constant.ANIM_ATTACK_FIRST);
            t.SetMove(Vector2.zero);
        }

        if (t.IsJumping)
        {
            t.Flip();
            t.ChangeAnim(S_Constant.ANIM_JUMP_UP_ATTACK);
        }
    }

    public void OnExecute(AbstractCharacter t)
    {
        timer += Time.deltaTime;

        t.PreAttack();

        if (!t.IsJumping)
        {
            if (timer > 0.5f)
            {
                t.ChangeAnim(S_Constant.ANIM_ATTACK_RECOVER);
            }

            if (timer > 1f)
            {
                t.ChangeState(AbstractCharacter.IDLE_STATE);
            }
        }
        else
        {
            if (timer > 1f)
            {
                if (!t.IsGrounded)
                {
                    t.ChangeState(AbstractCharacter.ON_AIR_STATE);
                }
            }
        }

        GatherAttackInput(t);
    }

    public void OnExit(AbstractCharacter t)
    {
        
    }

    // Unique function
    private void GatherAttackInput(AbstractCharacter t)
    {
        // Change to idle
        if (t.IsJumping && t.IsGrounded && t.rbVelocity.y < 0.1f)
        {
            t.SetBool(CharacterState.Jump, false);
            t.ChangeAnim(S_Constant.ANIM_JUMP_LANDING);
            t.ChangeState(AbstractCharacter.IDLE_STATE);
        }

        // Trigger another attack
        if (!t.IsJumping && Input.GetButtonDown("Attack") && comboPhase == 1)
        {
            comboPhase = 2;

            t.Flip();
            t.ChangeAnim(S_Constant.ANIM_ATTACK_SECOND);
            t.SetMove(Vector2.zero);
        }
    }
}
