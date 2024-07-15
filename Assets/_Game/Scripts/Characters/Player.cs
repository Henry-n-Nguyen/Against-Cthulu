using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : AbstractCharacter
{
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction attackAction;

    // Override Function
    public override void OnInit()
    {
        base.OnInit();

        moveAction = playerInput.actions.FindAction(Constant.INPUT_ACTION_MOVING);
    }

    public override void Move()
    {
        base.Move();

        horizontal = moveAction.ReadValue<Vector2>().x;

        Flip();

        characterTransform.position += Vector3.right * Time.deltaTime * speed * horizontal;

        if (Mathf.Abs(horizontal) <= 0.1f)
        {
            ChangeState(new IdleState());
        }
    }

    public override void Idle()
    {
        base.Idle();

        horizontal = moveAction.ReadValue<Vector2>().x;

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            ChangeState(new PatrolState());
        }
    }

    public override void ActiveAttack()
    {
        base.ActiveAttack();
    }

    public override void ActiveShoot()
    {
        base.ActiveShoot();
    }

    public override void Death()
    {
        base.Death();
    }
}
