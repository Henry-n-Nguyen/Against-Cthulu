using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PMoveState : IState<Player>
{
    public void OnEnter(Player t)
    {

    }

    public void OnExecute(Player t)
    {
        t.Move();

        if (Mathf.Abs(t.Horizontal) > 0f)
        {
            Vector2 moveVelocity = Vector2.right * t.Horizontal * t.RunSpeed + Vector2.up * t.RbVelocity.y;
            t.SetMove(moveVelocity);
        }

        GatherMovementInput(t);
    }

    public void OnExit(Player t)
    {
        if (!t.IsAttacking && !t.IsJumping && !t.IsHit && !t.IsSliding)
        {
            if (Mathf.Abs(t.RbVelocity.x) >= t.RunSpeed - 0.5f)
            {
                t.ChangeAnim(S_Constant.ANIM_RUN_END);
            }
            else
            {
                t.ChangeAnim(S_Constant.ANIM_IDLE);
            }
        }

        if (!t.IsHit && !t.IsAttacking && !t.IsSliding) t.SetBool(CharacterState.Run, false);
    }

    public void GatherMovementInput(Player t)
    {
        // Change to attack
        if (!t.IsAttacking && Input.GetButtonDown("Attack"))
        {
            t.SetBool(CharacterState.Attack, true);
            t.ChangeState(Player.ATTACK_STATE);
        }

        // Change to cast magic
        if (!t.IsAttacking && Input.GetButtonDown("Special1"))
        {
            t.SetBool(CharacterState.Attack, true);
            t.ChangeState(Player.CAST_MAGIC_STATE);
        }

        // Change to idle
        if (!Input.GetButton("Horizontal"))
        {
            t.ChangeState(Player.IDLE_STATE);
        }

        // Change to jump
        if (!t.IsJumping && Input.GetButtonDown("Jump"))
        {
            t.SetBool(CharacterState.Jump, true);

            Vector2 jumpVector = (Vector2.up + Vector2.right * t.Horizontal * 0.3f).normalized;
            t.Jump(jumpVector);

            t.ChangeState(Player.ON_AIR_STATE);
        }

        if (!t.IsGrounded)
        {
            t.SetBool(CharacterState.Jump, true);
            t.ChangeState(Player.ON_AIR_STATE);
        }

        // Change to Slide
        if (!t.IsSliding && Input.GetButtonDown("Slide"))
        {
            t.SetBool(CharacterState.Slide, true);
            t.ChangeState(Player.SLIDE_STATE);
        }
    }
}
