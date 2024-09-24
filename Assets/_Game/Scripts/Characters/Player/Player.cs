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

    [Header("Effect")]
    public CharacterEffect dustEffect;
    public CharacterEffect ghostEffect;

    // Bool Variables
    [field: Header("Boolean For Cooldown")]
    [field: SerializeField] public bool CanSpecial_01 { get; private set; } = true;
    [field: SerializeField] public bool CanSpecial_02 { get; private set; } = true;
    [field: SerializeField] public bool CanSlide { get; private set; } = true;

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
        currentState?.OnExit(this);

        if (currentState != null) prevState = currentState;

        currentState = state as IState<Player>;

        currentState?.OnEnter(this);
    }

    // State Function
    public override void Idle()
    {
        CheckIfShouldFlip();
    }

    public override void Move()
    {
        CheckIfShouldFlip();
        ChangeAnim(S_Constant.ANIM_RUN_LOOP);
    }

    public override void OnAir()
    {
        CheckIfShouldFlip();
    }

    public override void PreAttack()
    {
        CheckIfShouldFlip();
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

    public override void Flip()
    {
        base.Flip();
        if (IsGrounded) SpawnDustEffect();
    }

    public void Slide()
    {
        rb.velocity = characterTF.right * SlideForce;
        StartCoroutine(SetCooldown(CooldownState.Slide, 2f));
    }

    public void SpawnGhostEffect()
    {
        ghostEffect = SimplePool.Spawn<CharacterEffect>(PoolType.GhostEffect);
        ghostEffect.Init(this);
        ghostEffect.Spawn(characterTF);
    }

    public void SpawnDustEffect()
    {
        dustEffect = SimplePool.Spawn<CharacterEffect>(PoolType.DustEffect, characterTF);
        dustEffect.Init(this);
        dustEffect.Spawn(characterTF);
    }

    public IEnumerator SetCooldown(CooldownState state, float time)
    {
        switch (state)
        {
            case CooldownState.Special_01:
                CanSpecial_01 = false;
                break;
            case CooldownState.Special_02:
                CanSpecial_02 = false;
                break;
            case CooldownState.Slide:
                CanSlide = false;
                break;
            default: break;
        }

        yield return new WaitForSeconds(time);

        switch (state)
        {
            case CooldownState.Special_01:
                CanSpecial_01 = true;
                break;
            case CooldownState.Special_02:
                CanSpecial_02 = true;
                break;
            case CooldownState.Slide:
                CanSlide = true;
                break;
            default: break;
        }
    }
}
