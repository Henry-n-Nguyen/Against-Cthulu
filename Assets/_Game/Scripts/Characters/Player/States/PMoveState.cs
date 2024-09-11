using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class PMoveState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {

    }

    public void OnExecute(AbstractCharacter t)
    {
        t.Move();

        if (Mathf.Abs(t.horizontal) > 0f)
        {
            Vector2 moveVelocity = Vector2.right * t.horizontal * t.runSpeed + Vector2.up * t.rbVelocity.y;
            t.SetMove(moveVelocity);
        }

        GatherMovementInput(t);
    }

    public void OnExit(AbstractCharacter t)
    {
        if (t.IsRunning)
        {
            if (!t.IsJumping && !t.IsAttacking)
            {
                if (Mathf.Abs(t.rbVelocity.x) > t.runSpeed - 0.1f)
                {
                    t.ChangeAnim(S_Constant.ANIM_RUN_END);
                }
                else
                {
                    t.ChangeAnim(S_Constant.ANIM_IDLE);
                }

                t.SetMove(Vector2.zero);
            } 
        }
    }

    public void GatherMovementInput(AbstractCharacter t)
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

        // Change to idle
        if (!Input.GetButton("Horizontal"))
        {
            t.ChangeState(AbstractCharacter.IDLE_STATE);
        }

        // Change to jump
        if ((t.IsGrounded && Input.GetButtonDown("Jump") && !t.IsJumping) || !t.IsGrounded)
        {
            t.SetBool(CharacterState.Jump, true);
            t.ChangeState(AbstractCharacter.ON_AIR_STATE);
        }
    }
}
