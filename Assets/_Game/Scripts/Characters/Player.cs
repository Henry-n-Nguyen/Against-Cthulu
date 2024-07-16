using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AbstractCharacter
{
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction attackAction;
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private InputAction hitAction;

    private bool isSecondAttacked = false;

    private bool isAccelerated = false;
    private float tempHorizontal = 0f;

    // Override Function
    public override void OnInit()
    {
        horizontal = 0f;

        vertical = 0f;

        ChangeState(new IdleState());

        isAttacked = false;

        moveAction = playerInput.actions.FindAction(Constant.INPUT_ACTION_MOVING);
        attackAction = playerInput.actions.FindAction(Constant.INPUT_ACTION_ATTACKING);
        jumpAction = playerInput.actions.FindAction(Constant.INPUT_ACTION_JUMPING);
        hitAction = playerInput.actions.FindAction(Constant.INPUT_ACTION_HITTING);
    }

    public override void Move()
    {
        if (moveAction.WasReleasedThisFrame())
        {
            tempHorizontal = horizontal;
        }

        horizontal = moveAction.ReadValue<Vector2>().x;
        if (horizontal == tempHorizontal) isAccelerated = true;

        Flip();

        if (!IsGrounded()) return;

        isRunning = true;

        if (!isAccelerated)
        {
            ChangeAnim(Constant.TRIGGER_WALK);
            characterTransform.position += Vector3.right * Time.deltaTime * walkSpeed * horizontal;
        }
        else
        {
            ChangeAnim(Constant.TRIGGER_RUN_START);
            characterTransform.position += Vector3.right * Time.deltaTime * runSpeed * horizontal;
        }

        if (Mathf.Abs(horizontal) <= 0.1f)
        {
            if (isAccelerated)
            {
                ChangeState(new DeadState());

                isRunning = false;
                isAccelerated = false;
                tempHorizontal = 0f;

                ChangeAnim(Constant.TRIGGER_RUN_END);
            }
            else
            {
                ChangeState(new IdleState());
            }
        }

        vertical = moveAction.ReadValue<Vector2>().y;

        if (attackAction.WasPressedThisFrame())
        {
            if (Mathf.Abs(vertical) <= 0.1f)
            {
                ChangeState(new AttackState());
                Attack();
            }
            else if (vertical > 0.1f)
            {
                isJumping = true;
                ChangeAnim(Constant.TRIGGER_JUMP_UP_ATTACK);

                horizontal = moveAction.ReadValue<Vector2>().x;

                if (Mathf.Abs(horizontal) <= 0.1f) rb.velocity = Vector2.up * jumpingPower;
                else
                {
                    rb.velocity = Vector2.up * jumpingPower + Vector2.right * jumpingPower * 0.66f * horizontal;
                }

                ChangeState(new JumpState());
            }
        }

        if (jumpAction.WasPressedThisFrame())
        {
            ChangeState(new JumpState());
            Jump();
        }

        if (hitAction.WasPressedThisFrame())
        {
            ChangeState(new HitState());
            Hit();
        }
    }

    public override void Idle()
    {
        if (!IsGrounded()) return;

        isJumping = false;
        isRunning = false;
        ChangeAnim(Constant.TRIGGER_IDLE);

        horizontal = moveAction.ReadValue<Vector2>().x;

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeState(new MoveState());
        }

        vertical = moveAction.ReadValue<Vector2>().y;

        if (attackAction.WasPressedThisFrame())
        {
            if (Mathf.Abs(vertical) <= 0.1f)
            {
                ChangeState(new AttackState());
                Attack();
            }
            else if (vertical > 0.1f)
            {
                isJumping = true;
                ChangeAnim(Constant.TRIGGER_JUMP_UP_ATTACK);

                horizontal = moveAction.ReadValue<Vector2>().x;

                if (Mathf.Abs(horizontal) <= 0.1f) rb.velocity = Vector2.up * jumpingPower;
                else
                {
                    rb.velocity = Vector2.up * jumpingPower + Vector2.right * jumpingPower * 0.66f * horizontal;
                }

                ChangeState(new JumpState());
            }
        }

        if (jumpAction.WasPressedThisFrame())
        {
            ChangeState(new JumpState());
            Jump();
        }

        if (hitAction.WasPressedThisFrame())
        {
            ChangeState(new HitState());
            Hit();
        }
    }

    public override void Jump()
    {
        if (isAccelerated) jumpingPower = 7f;
        else jumpingPower = 6f;
        
        if (!isJumping)
        {
            isJumping = true;
            ChangeAnim(Constant.TRIGGER_JUMP_START);

            horizontal = moveAction.ReadValue<Vector2>().x;

            if (Mathf.Abs(horizontal) <= 0.1f) rb.velocity = Vector2.up * jumpingPower;
            else
            {
                rb.velocity = Vector2.up * jumpingPower + Vector2.right * jumpingPower * 0.66f * horizontal;
            }
        }

        if (rb.velocity.y < 0 && !IsGrounded())
        {
            ChangeAnim(Constant.TRIGGER_JUMP_END);
        }
        else if (rb.velocity.y < 0 && IsGrounded())
        {
            ChangeState(new DeadState());
            isJumping = false;
            ChangeAnim(Constant.TRIGGER_JUMP_LANDING);
        }
        
        if (isJumping && attackAction.WasPressedThisFrame())
        {
            ChangeState(new DeadState());
            isJumping = false;
            ChangeAnim(Constant.TRIGGER_JUMP_ATTACK);
        }

        if (hitAction.WasPressedThisFrame())
        {
            ChangeState(new HitState());
            Hit();
        }
    }

    public override void Attack()
    {
        if (isSecondAttacked) return;

        if (!attackAction.WasReleasedThisFrame()) return;

        if (isAttacked)
        {
            ChangeAnim(Constant.TRIGGER_ATTACK_SECOND);
            isSecondAttacked = true;
        }
        else
        {
            isAttacked = true;
            ChangeAnim(Constant.TRIGGER_ATTACK_FIRST);
            ChangeAnim(Constant.TRIGGER_ATTACK_RECOVER);
        }

        if (hitAction.WasPressedThisFrame())
        {
            ChangeState(new HitState());
            Hit();
        }
    }

    public override void ResetState()
    {
        base.ResetState();

        isSecondAttacked = false;
    }

    public override void Death()
    {
        base.Death();
    }

    public override void Hit()
    {
        if (isJumping)
        {
            ChangeAnim(Constant.TRIGGER_JUMP_HIT);

            if (rb.velocity.y < 0 && IsGrounded())
            {
                ChangeState(new DeadState());
                ChangeAnim(Constant.TRIGGER_JUMP_HIT_GROUND);
            }
        }
        else
        {
            ChangeAnim(Constant.TRIGGER_HIT);
        }
    }

    public void JumpingEnd()
    {
        isJumping = false;
    }
}
