using HuySpace;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class POnAirState : IState<AbstractCharacter>
{
    private float timer;

    private bool isAttackedBefore;

    private bool isFalling;

    public void OnEnter(AbstractCharacter t)
    {
        timer = 0f;

        isFalling = false;

        t.SetBool(CharacterState.Attack, false);
        t.SetBool(CharacterState.Run, false);

        if (t.IsGrounded && t.IsJumping)
        {
            isAttackedBefore = false;

            Vector2 jumpVector = (Vector2.up + Vector2.right * t.horizontal * 0.3f).normalized;
            t.SetMove(Vector2.zero);
            t.Jump(jumpVector);
        }
    }

    public void OnExecute(AbstractCharacter t)
    {
        timer += Time.deltaTime;

        if (!t.IsAttacking)
        {
            if (t.rbVelocity.y < -0.01f)
            {
                if (!isFalling) isFalling = true;
                Falling(t);
            }
            else if (t.rbVelocity.y > 0.01f) Jumping(t);

            t.OnAir();

            if (Mathf.Abs(t.horizontal) > 0f)
            {
                Vector2 moveVelocity = Vector2.right * t.horizontal * t.runSpeed + Vector2.up * t.rbVelocity.y;
                t.SetMove(moveVelocity);
            }
        }

        GatherOnAirInput(t);
    }

    public void OnExit(AbstractCharacter t)
    {

    }

    public void GatherOnAirInput(AbstractCharacter t)
    {
        // Change to idle
        if (t.IsGrounded && t.rbVelocity.y < 0.1f)
        {
            t.ChangeAnim(S_Constant.ANIM_JUMP_LANDING);
            t.ChangeState(AbstractCharacter.IDLE_STATE);
        }

        // Change to jump-up attack
        if (t.IsJumping && !isAttackedBefore && Input.GetButtonDown("Attack") && !isFalling)
        {
            isAttackedBefore = true;
            t.SetBool(CharacterState.Attack, true);
            t.ChangeState(AbstractCharacter.ATTACK_STATE);
        }

        // Change to jump-cast magic attack
        if (t.IsJumping && Input.GetButtonDown("Special1"))
        {
            t.ChangeState(AbstractCharacter.CAST_MAGIC_STATE);
        }
    }

    protected void Jumping(AbstractCharacter t)
    {
        t.ChangeAnim(S_Constant.ANIM_JUMP_START);
    }

    protected void Falling(AbstractCharacter t)
    {
        t.ChangeAnim(S_Constant.ANIM_JUMP_END);
    }
}
