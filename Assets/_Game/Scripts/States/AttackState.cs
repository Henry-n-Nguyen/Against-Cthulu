using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

public class AttackState : IState<AbstractCharacter>
{
    private int comboPhase = 1;

    private float timer;

    public void OnEnter(AbstractCharacter t)
    {
        timer = 0f;
        comboPhase = 1;

        if (t.isGrounded && !t.isJumping)
        {
            t.Flip();
            t.ChangeAnim(Constant.ANIM_ATTACK_FIRST);
            t.rb.velocity = Vector2.zero;
            t.rb.AddForce(Vector2.right * t.horizontal * t.walkSpeed, ForceMode2D.Impulse);
        }

        if (t.isJumping)
        {
            t.Flip();
            t.ChangeAnim(Constant.ANIM_JUMP_UP_ATTACK);
        }
    }

    public void OnExecute(AbstractCharacter t)
    {
        timer += Time.deltaTime;

        t.Attack();

        if (!t.isJumping)
        {
            if (timer > 0.5f)
            {
                t.ChangeAnim(Constant.ANIM_ATTACK_RECOVER);
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
                if (!t.isGrounded)
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
        if (t.isJumping && t.isGrounded && t.rb.velocity.y < 0.1f)
        {
            t.isJumping = false;
            t.ChangeAnim(Constant.ANIM_JUMP_LANDING);
            t.ChangeState(AbstractCharacter.IDLE_STATE);
        }

        // Trigger another attack
        if (!t.isJumping && Input.GetButtonDown("Attack") && comboPhase == 1)
        {
            comboPhase = 2;

            t.Flip();
            t.ChangeAnim(Constant.ANIM_ATTACK_SECOND);
            t.rb.velocity = Vector2.zero;
            t.rb.AddForce(Vector2.right * t.horizontal * t.walkSpeed, ForceMode2D.Impulse);
        }
    }
}
