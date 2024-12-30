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

    public event Action OnHaveUprades;
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

    public float ReduceCooldown { get; private set; }

    // private variable
    private UserData userData;

    private IState<Player> currentState;
    private IState<Player> prevState;
    private CharacterEffect dustEffect;
    private CharacterEffect ghostEffect;

    private PlayerMagicIndex currentMagicIndex = PlayerMagicIndex.First;

    public Magic magic_1;
    public Magic magic_2;

    public Equipment weapon;
    public Equipment belt;

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
        damageable.MaxHP = userData.HP;
        damageable.OnInit();
        IsHitOrHealEvent?.Invoke();

        NormalDamage = userData.DMG;

        Speed = userData.SPD;
        JumpForce = Speed * 1.5f;
        SlideForce = Speed * 3f;

        ReduceCooldown = userData.R_CD;
    }

    public override void OnInit()
    {
        userData = UserDataManager.Ins.userData;

        magic_1 = null;
        magic_2 = null;

        weapon = null;
        belt = null;

        InitStat();
        LoadData();
        OnSwapSkill();
        ChangeState(IDLE_STATE);
    }

    public void LoadData()
    {
        UserData data = UserDataManager.Ins.userData;

        magic_1 = data.Skill_1;
        if (magic_1 == null) CanSpecial_01 = false;
        else CanSpecial_01 = true;

        magic_2 = data.Skill_2;
        if (magic_1 == null || magic_2 == null) CanSpecial_02 = false;
        else if (magic_2 != null) CanSpecial_02 = true;

        currentMagicIndex = PlayerMagicIndex.First;
        currentMagicName = magic_1 == null ? MagicName.None : (MagicName)magic_1.poolType;
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
            target.Hit(NormalDamage);
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
        StartCoroutine(GamePlayManager.Ins.OnResult());

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

    public void ChooseMagic(PlayerMagicIndex index)
    {
        if (currentMagicIndex == index)
        {
            switch (currentMagicIndex)
            {
                case PlayerMagicIndex.First:
                    if (magic_1 != null) currentMagicName = (MagicName)magic_1.poolType;
                    return;
                case PlayerMagicIndex.Second:
                    if (magic_2 != null) currentMagicName = (MagicName)magic_2.poolType;
                    return;
            }
        }

        switch (currentMagicIndex)
        {
            case PlayerMagicIndex.First:
                currentMagicIndex = PlayerMagicIndex.Second;
                if (magic_2 != null) currentMagicName = (MagicName)magic_2.poolType;
                return;
            case PlayerMagicIndex.Second:
                currentMagicIndex = PlayerMagicIndex.First;
                if (magic_1 != null) currentMagicName = (MagicName)magic_1.poolType;
                return;
        }
    } 

    public void Slide()
    {
        rb.velocity = characterTF.right * SlideForce;
        StartCoroutine(SetCooldown(CooldownState.Slide, 2f * (1 - ReduceCooldown)));
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

        StartCoroutine(SetCooldown((CooldownState)currentMagicIndex, currentMagic.CD * (1 - ReduceCooldown)));
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
        ChooseMagic(currentMagicIndex);

        OnSkill_Swap?.Invoke();
    }

    public void OnUpdated()
    {
        InitStat();

        OnHaveUprades?.Invoke();
    }

    // Music Function
    public void PlayPlayerSFX(SFX index)
    {
        AudioManager.Ins.PlaySFX(index);
    }
}
