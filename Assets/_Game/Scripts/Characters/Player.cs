using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AbstractCharacter
{
    // Override Function
    public override void OnInit()
    {
        base.OnInit();
    }
    public override void Idle()
    {
        base.Idle();

        GatherIdleInput();
    }

    public override void Move()
    {
        base.Move();

        if (Mathf.Abs(horizontal) > 0f)
        {
            rb.velocity = Vector2.right * horizontal * walkSpeed + Vector2.up * rb.velocity.y;
        }

        GatherMovementInput();
    }

    public override void OnAir()
    {
        base.OnAir();

        if (Mathf.Abs(horizontal) > 0f)
        {
            rb.velocity = Vector2.right * horizontal * walkSpeed + Vector2.up * rb.velocity.y;
        }

        GatherOnAirInput();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public override void Death()
    {
        base.Death();
    }

    public override void Hit()
    {
        base.Hit();
    }


    // Gather Input
    public void GatherIdleInput()
    {
        // Change to move
        if (isGrounded && Input.GetButton("Horizontal"))
        {
            isRunning = true;
            ChangeState(MOVE_STATE);
        }

        // Change to jump
        if ((isGrounded && Input.GetButtonDown("Jump") && !isJumping) || !isGrounded)
        {
            isJumping = true;
            ChangeState(ON_AIR_STATE);
        }

        // Change to attack
    }

    public void GatherMovementInput()
    {
        // Change to idle
        if (!Input.GetButton("Horizontal"))
        {
            ChangeState(IDLE_STATE);
        }

        // Change to jump
        if ((isGrounded && Input.GetButtonDown("Jump") && !isJumping) || !isGrounded)
        {
            isJumping = true;
            ChangeState(ON_AIR_STATE);
        }
    }

    public void GatherOnAirInput()
    {
        // Change to idle
        if (isGrounded && rb.velocity.y < 0.1f)
        {
            ChangeState(IDLE_STATE);
        }
    }
}
