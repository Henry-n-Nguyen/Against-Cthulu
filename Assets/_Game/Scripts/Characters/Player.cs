using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AbstractCharacter
{
    private float tempHorizontal = 0f;

    // Override Function
    public override void OnInit()
    {
        horizontal = 0f;

        vertical = 0f;

        ChangeState(new IdleState());

        isAttacked = false;
    }
    public override void Idle()
    {
        base.Idle();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Jump()
    {
        base.Jump();
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
}
