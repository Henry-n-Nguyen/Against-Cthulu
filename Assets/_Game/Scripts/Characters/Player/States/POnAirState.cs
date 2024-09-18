using HuySpace;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using UnityEngine;

public class POnAirState : IState<Player>
{
    private float timer;

    private bool isAttackedBefore;

    private bool isFalling;
    private bool isDoubleJumping;

    public void OnEnter(Player t)
    {
        timer = 0f;

        isFalling = false;
        isDoubleJumping = false;
        isAttackedBefore = false;

        t.SetBool(CharacterState.Attack, false);
        t.SetBool(CharacterState.Run, false);
    }

    public void OnExecute(Player t)
    {
        timer += Time.deltaTime;

        if (t.RbVelocity.y < -0.01f)
        {
            if (!isFalling) isFalling = true;
            Falling(t);
        }
        else if (t.RbVelocity.y > 0.01f) Jumping(t);

        t.OnAir();

        if (Mathf.Abs(t.Horizontal) > 0f)
        {
            Vector2 moveVelocity = Vector2.right * t.Horizontal * t.RunSpeed + Vector2.up * t.RbVelocity.y;
            t.SetMove(moveVelocity);
        }

        GatherOnAirInput(t);
    }

    public void OnExit(Player t)
    {
        
    }

    public void GatherOnAirInput(Player t)
    {
        // Make stop on air when release jump button (Jump Cutting)
        if (Input.GetButtonUp("Jump") && !isFalling)
        {
            t.SetMove(Vector2.zero + Vector2.right * t.RbVelocity.x);
        }

        // Double jump
        if (Input.GetButtonDown("Jump") && !isDoubleJumping)
        {
            isFalling = false;
            isDoubleJumping = true;
            Vector2 jumpVector = (Vector2.up + Vector2.right * t.Horizontal * 0.3f).normalized;
            t.SetMove(Vector2.zero + Vector2.up * t.RbVelocity.y);
            t.Jump(jumpVector);
        }

        // Change to idle
        if (t.IsGrounded && t.RbVelocity.y < 0.1f)
        {
            t.SetBool(CharacterState.Jump, false);
            if (isFalling) t.ChangeAnim(S_Constant.ANIM_JUMP_LANDING);
            else t.ChangeAnim(S_Constant.ANIM_IDLE);
            t.ChangeState(Player.IDLE_STATE);
        }

        // Change to jump-up attack
        if (!isFalling && !isAttackedBefore && Input.GetButtonDown("Attack"))
        {
            isAttackedBefore = true;
            t.SetBool(CharacterState.Attack, true);
            t.ChangeState(Player.ATTACK_STATE);
        }

        // Change to jump-cast magic attack
        if (Input.GetButtonDown("Special1"))
        {
            t.SetBool(CharacterState.Attack, true);
            t.ChangeState(Player.CAST_MAGIC_STATE);
        }

        // Change to Slide
        if (!t.IsSliding && Input.GetButtonDown("Slide"))
        {
            t.SetBool(CharacterState.Slide, true);
            t.ChangeState(Player.SLIDE_STATE);
        }
    }

    protected void Jumping(Player t)
    {
        t.ChangeAnim(S_Constant.ANIM_JUMP_START);
    }

    protected void Falling(Player t)
    {
        t.ChangeAnim(S_Constant.ANIM_JUMP_END);
    }
}
