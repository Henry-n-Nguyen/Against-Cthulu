using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class OnAirState : IState<AbstractCharacter>
{
    private float timer;

    private bool isAttackedBefore;

    public void OnEnter(AbstractCharacter t)
    {
        timer = 0f;

        t.isAttacking = false;
        t.isRunning = false;

        if (t.isGrounded && t.isJumping)
        {
            isAttackedBefore = false;

            Vector2 jumpVector = (Vector2.up + Vector2.right * t.horizontal * 0.3f).normalized;
            t.rb.velocity = Vector2.zero;
            t.rb.AddForce(jumpVector * t.jumpForce, ForceMode2D.Impulse);
        }
    }

    public void OnExecute(AbstractCharacter t)
    {
        timer += Time.deltaTime;

        if (!t.isAttacking)
        {
            if (t.rb.velocity.y < -0.01f) Falling(t);
            else if (t.rb.velocity.y > 0.01f) Jumping(t);

            t.OnAir();

            if (Mathf.Abs(t.horizontal) > 0f)
            {
                t.rb.velocity = Vector2.right * t.horizontal * t.walkSpeed + Vector2.up * t.rb.velocity.y;
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
        if (t.isGrounded && t.rb.velocity.y < 0.1f)
        {
            t.isJumping = false;
            t.ChangeAnim(Constant.ANIM_JUMP_LANDING);
            t.ChangeState(AbstractCharacter.IDLE_STATE);
        }

        // Change to jump-up attack
        if (t.isJumping && !isAttackedBefore && Input.GetButtonDown("Attack"))
        {
            isAttackedBefore = true;
            t.isAttacking = true;
            t.ChangeState(AbstractCharacter.ATTACK_STATE);
        }
    }

    protected void Jumping(AbstractCharacter t)
    {
        t.ChangeAnim(Constant.ANIM_JUMP_START);
    }

    protected void Falling(AbstractCharacter t)
    {
        t.ChangeAnim(Constant.ANIM_JUMP_END);
    }
}
