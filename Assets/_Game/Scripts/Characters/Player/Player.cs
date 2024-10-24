using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using HuySpace;
using TMPro;
using System;

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

    public event Action IsHitOrHealEvent;
    public event Action OnSkill_01_CooldownEvent;
    public event Action OnSkill_02_CooldownEvent;
    public event Action OnSkill_Swap;

    // Bool Variables
    [field: Header("Boolean For Cooldown")]
    [field: SerializeField] public bool CanSpecial_01 { get; private set; } = true;
    [field: SerializeField] public bool CanSpecial_02 { get; private set; } = true;
    [field: SerializeField] public bool CanSlide { get; private set; } = true;
    [field: SerializeField] public bool CanMove { get; private set; } = true;

    [Header("SO Config")]
    [SerializeField] private PlayerConfigSO config;

    [HideInInspector] public Sprite special_1_icon { get; private set; } = null;
    [HideInInspector] public Sprite special_2_icon { get; private set; } = null;

    // private variable
    private IState<Player> currentState;
    private IState<Player> prevState;
    private CharacterEffect dustEffect;
    private CharacterEffect ghostEffect;

    private PlayerMagicIndex currentMagicIndex = PlayerMagicIndex.First;
    private MagicName special_1;
    private MagicName special_2;

    void Update()
    {
        GatherInput();

        currentState?.OnExecute(this);
    }

    private void GatherInput()
    {
        CheckGround();

        Horizontal = Input.GetAxis("Horizontal");
    }

    private void InitStat()
    {
        Speed = config.Speed;
        JumpForce = config.JumpForce;
        SlideForce = config.SlideForce;
        NormalDamage = config.NormalDamage;
    }

    public override void OnInit()
    {
        InitStat();
        LoadData();
        OnSwapSkill();
        ChangeState(IDLE_STATE);
    }

    public void LoadData()
    {
        UserData data = UserDataManager.Ins.userData;

        Magic magic;

        special_1 = data.special_1;
        if (special_1 == MagicName.None) CanSpecial_01 = false;
        else
        {
            magic = SimplePool.Spawn<Magic>((PoolType)special_1);
            special_1_icon = magic.GetMagicSprite(); 
        }

        special_2 = data.special_2;
        if (special_1 == MagicName.None || special_2 == MagicName.None) CanSpecial_02 = false;
        else
        {
            magic = SimplePool.Spawn<Magic>((PoolType)special_2);
            special_2_icon = magic.GetMagicSprite();
        }

        currentMagicIndex = PlayerMagicIndex.First;
        currentMagicName = special_1;
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

    public override void Attack()
    {
        List<Damageable> targetInRange = attackZone.GetTargetList();
        foreach (Damageable target in targetInRange)
        {
            target.Hit(config.NormalDamage);
        }
    }

    public override void Death()
    {
        base.Death();
    }

    public override void Hit()
    {
        if (IsHit)
        {
            return;
        }

        IsHitOrHealEvent?.Invoke();

        SetBool(CharacterState.Hit, true);
        ChangeState(HIT_STATE);
    }

    public override void Die()
    {
        IsHitOrHealEvent?.Invoke();

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

    public void SwapSpecial(PlayerMagicIndex index)
    {
        if (currentMagicIndex == index)
        {
            if (currentMagicIndex == PlayerMagicIndex.First)
            {
                currentMagicName = special_1;
            }
            else
            {
                currentMagicName = special_2;
            }
            return;
        }

        if (currentMagicIndex == PlayerMagicIndex.First)
        {
            currentMagicIndex = PlayerMagicIndex.Second;
            currentMagicName = special_2;
        }
        else
        {
            currentMagicIndex = PlayerMagicIndex.First;
            currentMagicName = special_1;
        }
    } 

    public void Slide()
    {
        rb.velocity = characterTF.right * SlideForce;
        StartCoroutine(SetCooldown(CooldownState.Slide, 2f));
    }

    public override void CastMagic()
    {
        currentMagic = SimplePool.Spawn<Magic>((PoolType)currentMagicName);
        currentMagic.InitOwner(this);

        switch (currentMagic.DeployType)
        {
            case MagicDeployType.Floating:
                currentMagic.Spawn(shootTF);
                break;
            case MagicDeployType.InGround:
                currentMagic.Spawn(inGroundShootTF);
                break;
        }

        if (currentMagicIndex == PlayerMagicIndex.First)
        {
            OnSkill_01_CooldownEvent?.Invoke();
        }
        else
        {
            OnSkill_02_CooldownEvent?.Invoke();
        }

        StartCoroutine(SetCooldown((CooldownState)currentMagicIndex, currentMagic.CD));
    }

    public void Freeze(bool value)
    {
        CanMove = !value;
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

    public void OnSwapSkill()
    {
        UserData data = UserDataManager.Ins.userData;

        LoadData();
        SwapSpecial(currentMagicIndex);

        OnSkill_Swap?.Invoke();
    }
}
