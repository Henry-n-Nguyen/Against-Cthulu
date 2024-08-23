using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class MoveState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {

    }

    public void OnExecute(AbstractCharacter t)
    {
        t.Move();

        if (Mathf.Abs(t.horizontal) > 0f)
        {
            t.rb.velocity = Vector2.right * t.horizontal * t.walkSpeed + Vector2.up * t.rb.velocity.y;
        }

        GatherMovementInput(t);
    }

    public void OnExit(AbstractCharacter t)
    {
        if (t.isRunning)
        {
            if (!t.isJumping && !t.isAttacking)
            {
                if (Mathf.Abs(t.rb.velocity.x) > t.walkSpeed - 0.1f)
                {
                    t.ChangeAnim(Constant.ANIM_RUN_END);
                }
                else
                {
                    t.ChangeAnim(Constant.ANIM_IDLE);
                }

                t.rb.velocity = Vector2.zero;
            } 
        }
    }

    public void GatherMovementInput(AbstractCharacter t)
    {
        // Change to idle
        if (!Input.GetButton("Horizontal"))
        {
            t.ChangeState(AbstractCharacter.IDLE_STATE);
        }

        // Change to jump
        if ((t.isGrounded && Input.GetButtonDown("Jump") && !t.isJumping) || !t.isGrounded)
        {
            t.isJumping = true;
            t.ChangeState(AbstractCharacter.ON_AIR_STATE);
        }

        // Change to attack
        if (t.isGrounded && Input.GetButtonDown("Attack"))
        {
            t.isAttacking = true;
            t.ChangeState(AbstractCharacter.ATTACK_STATE);
        }
    }
}
