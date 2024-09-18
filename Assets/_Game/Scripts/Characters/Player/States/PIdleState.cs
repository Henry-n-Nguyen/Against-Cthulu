using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PIdleState : IState<Player>
{
    public void OnEnter(Player t)
    {
        t.SetMove(Vector2.zero + Vector2.up * t.RbVelocity.y);
    }

    public void OnExecute(Player t)
    {
        t.Idle();

        GatherIdleInput(t);
    }

    public void OnExit(Player t)
    {

    }

    private void GatherIdleInput(Player t)
    {
        // Change to attack
        if (t.IsGrounded && !t.IsAttacking && Input.GetButtonDown("Attack"))
        {
            t.SetBool(CharacterState.Attack, true);
            t.ChangeState(Player.ATTACK_STATE);
        }

        // Change to cast magic
        if (t.IsGrounded && !t.IsAttacking && Input.GetButtonDown("Special1"))
        {
            t.SetBool(CharacterState.Attack, true);
            t.ChangeState(Player.CAST_MAGIC_STATE);
        }

        // Change to move
        if (t.IsGrounded && !t.IsAttacking && Input.GetButton("Horizontal"))
        {
            t.SetBool(CharacterState.Run, true);
            t.ChangeState(Player.MOVE_STATE);
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
