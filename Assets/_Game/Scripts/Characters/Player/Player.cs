using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HuySpace;

public class Player : AbstractCharacter
{
    // Constant
    public static PIdleState IDLE_STATE = new PIdleState();
    public static PMoveState MOVE_STATE = new PMoveState();
    public static POnAirState ON_AIR_STATE = new POnAirState();
    public static PAttackState ATTACK_STATE = new PAttackState();
    public static PCastMagicState CAST_MAGIC_STATE = new PCastMagicState();
    public static PSlideState SLIDE_STATE = new PSlideState();
    public static PHitState HIT_STATE = new PHitState();
    public static PDeadState DEAD_STATE = new PDeadState();

    // private variable
    private IState<Player> currentState;
    private IState<Player> prevState;

    void Update()
    {
        GatherInput();

        currentState?.OnExecute(this);
    }

    private void GatherInput()
    {
        CheckGround();

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
    }

    public override void OnInit()
    {
        ChangeState(IDLE_STATE);
    }

    public override void ChangeState<T>(IState<T> state)
    {
        Debug.Log(state);

        currentState?.OnExit(this);

        if (currentState != null) prevState = currentState;

        currentState = state as IState<Player>;

        currentState?.OnEnter(this);
    }

    // State Function
    public override void Idle()
    {
        Flip();
    }

    public override void Move()
    {
        Flip();
        ChangeAnim(S_Constant.ANIM_RUN_LOOP);
    }

    public override void OnAir()
    {
        Flip();
    }

    public override void PreAttack()
    {
        Flip();
    }

    public override void Death()
    {
        base.Death();
    }

    public override void Hit()
    {
        SetBool(CharacterState.Hit, true);
        ChangeState(HIT_STATE);
    }

    public override void Die()
    {
        ChangeState(DEAD_STATE);
    }

    public override void CallBackState()
    {
        SetMove(Vector2.zero);
        ChangeState(prevState);
    }

    public void Slide()
    {
        rb.velocity = characterTF.right * SlideForce;
    }

    public void BackEffect()
    {

    }
}
