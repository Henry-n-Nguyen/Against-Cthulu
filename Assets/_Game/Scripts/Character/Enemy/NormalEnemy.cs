using HuySpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemy : AbstractEnemy
{
    [Header("SO Config")]
    [SerializeField] private NormalEnemyConfigSO config;

    private float stateTimer = 0f;
    private float attackCooldown = 0f;

    public event Action OnDeath;

    // Override Function

    private void Awake()
    {
        InitStat();
    }

    private void InitStat()
    {
        damageable.MaxHP = config.HP;
        Speed = config.Speed;
        NormalDamage = config.NormalDamage;
    }

    public override void Idle() 
    { 
        base.Idle();

        SetMove(Vector2.zero + Vector2.up * RbVelocity.y);

        stateTimer += Time.deltaTime;
        attackCooldown += Time.deltaTime;

        if (HasEnemyInAttackRange && attackCooldown > config.ATTACK_COOLDOWN_TIME)
        {
            stateTimer = 0f;
            attackCooldown = 0f;

            SetMove(Vector2.zero + Vector2.up * RbVelocity.y);

            ChangeState(E_ATTACK_STATE);
        }
        else if (DetectedTarget)
        {
            stateTimer = 0f;

            SetBool(CharacterState.Run, true);

            ChangeState(E_MOVE_STATE);
        }
        else if (stateTimer >= config.IDLE_TIME)
        {
            stateTimer = 0f;

            Horizontal = UnityEngine.Random.Range(0, 2) == 0 ? 1f : -1f;

            SetBool(CharacterState.Run, true);

            ChangeState(E_MOVE_STATE);
        }
    }

    public override void Move() 
    { 
        base.Move();

        SetMove(characterTF.right * config.Speed + Vector3.up * RbVelocity.y);

        stateTimer += Time.deltaTime;
        attackCooldown += Time.deltaTime;

        if (stateTimer >= config.PATROL_TIME)
        {
            stateTimer = 0f;

            ChangeState(E_IDLE_STATE);
        }

        if (HasEnemyInAttackRange && attackCooldown > config.ATTACK_COOLDOWN_TIME)
        {
            stateTimer = 0f;
            attackCooldown = 0f;

            SetMove(Vector2.zero + Vector2.up * RbVelocity.y);

            ChangeState(E_ATTACK_STATE);
        }
    }

    public override void PreAttack() 
    {
        if (!IsAttacking)
        {
            attackCooldown = 0f;

            SetBool(CharacterState.Attack, true);

            base.PreAttack();

            ChangeAnim(S_Constant.ANIM_ATTACK);
        }
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
        OnDeath?.Invoke();

        base.Death();
    }

    public override void Hit() 
    {
        base.Hit();
    }

    public override void Drop()
    {
        float rate = UnityEngine.Random.Range(0, 100);

        int coins = 2;
        int rubys = 1;

        if (rate < 10) // Event give ruby with rate 10%
        {
            for (int i = 0; i < rubys; i++)
            {
                SimplePool.Spawn<Collectable>(PoolType.Collectable_Diamond, characterTF.position, Quaternion.identity);
            }
        }
        else if (rate < 40) // Event give coin with rate 30%
        {
            for (int i = 0; i < coins; i++)
            {
                SimplePool.Spawn<Collectable>(PoolType.Collectable_Coin, characterTF.position, Quaternion.identity);
            }
        }
    }
}
