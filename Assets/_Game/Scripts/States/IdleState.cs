using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class IdleState : IState<AbstractCharacter>
{
    public void OnEnter(AbstractCharacter t)
    {
        t.isAttacking = false;
        t.isRunning = false;
        t.isJumping = false;
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
        // Change to move
        if (t.isGrounded && !t.isAttacking && Input.GetButton("Horizontal"))
        {
            t.isRunning = true;
            t.ChangeState(AbstractCharacter.MOVE_STATE);
        }

        // Change to jump
        if ((t.isGrounded && !t.isAttacking && !t.isJumping && Input.GetButtonDown("Jump")) || !t.isGrounded)
        {
            t.isJumping = true;
            t.ChangeState(AbstractCharacter.ON_AIR_STATE);
        }

        // Change to attack
        if (t.isGrounded && !t.isAttacking && Input.GetButtonDown("Attack"))
        {
            t.isAttacking = true;
            t.ChangeState(AbstractCharacter.ATTACK_STATE);
        }
    }
}
