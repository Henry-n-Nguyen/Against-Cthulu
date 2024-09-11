using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PIdleState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {
        t.SetBool(CharacterState.Attack, false);
        t.SetBool(CharacterState.Run, false);
        t.SetBool(CharacterState.Jump, false);
        t.SetMove(Vector2.zero);
    }

    public void OnExecute(AbstractCharacter t)
    {
        t.Idle();

        GatherIdleInput(t);
    }

    public void OnExit(AbstractCharacter t)
    {

    }

    private void GatherIdleInput(AbstractCharacter t)
    {
        // Change to attack
        if (t.IsGrounded && !t.IsAttacking && Input.GetButtonDown("Attack"))
        {
            t.SetBool(CharacterState.Attack, true);
            t.ChangeState(AbstractCharacter.ATTACK_STATE);
        }

        // Change to cast magic
        if (t.IsGrounded && !t.IsAttacking && Input.GetButtonDown("Special1"))
        {
            t.SetBool(CharacterState.Attack, true);
            t.ChangeState(AbstractCharacter.CAST_MAGIC_STATE);
        }

        // Change to move
        if (t.IsGrounded && !t.IsAttacking && Input.GetButton("Horizontal"))
        {
            t.SetBool(CharacterState.Run, true);
            t.ChangeState(AbstractCharacter.MOVE_STATE);
        }

        // Change to jump
        if ((t.IsGrounded && !t.IsAttacking && !t.IsJumping && Input.GetButtonDown("Jump")) || !t.IsGrounded)
        {
            t.SetBool(CharacterState.Jump, true);
            t.ChangeState(AbstractCharacter.ON_AIR_STATE);
        }

    }
}
