using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;
using System;
using System.Xml;

public class BossEnemy : AbstractEnemy
{
    [Header("SO Config")]
    [SerializeField] private BossEnemyConfigSO config;

    [SerializeField] private MagicName attack_1;
    [SerializeField] private MagicName attack_2;
    [SerializeField] private MagicName attack_3;

    public event Action IsHitOrHealEvent;
    public event Action IsDeathEvent;

    [field: SerializeField] public Sprite Portrait { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    private float stateTimer = 0f;

    private bool isAttackAfterMove;

    // Override Function
    public override void OnInit()
    {
        Horizontal = -1;
        isAttackAfterMove = false;

        InitStat();

        base.OnInit();
    }

    private void InitStat()
    {
        Portrait = config.Portrait;
        Name = config.Name;

        damageable.MaxHP = config.HP;
        Speed = config.Speed;
        JumpForce = config.JumpForce;
        NormalDamage = config.NormalDamage;
    }

    public override void Idle()
    {
        base.Idle();

        SetMove(Vector2.zero + Vector2.up * RbVelocity.y);

        stateTimer += Time.deltaTime;

        if (!isAttackAfterMove)
        {
            isAttackAfterMove = true;

            stateTimer = 0f;

            SetMove(Vector2.zero + Vector2.up * RbVelocity.y);

            ChangeState(E_ATTACK_STATE);
        }

        if (stateTimer >= config.IDLE_TIME)
        {
            stateTimer = 0f;

            SetBool(CharacterState.Run, true);

            ChangeState(E_MOVE_STATE);
        }
    }

    public override void Move()
    {
        base.Move();

        SetMove(characterTF.right * config.Speed + Vector3.up * RbVelocity.y);

        stateTimer += Time.deltaTime;

        if (stateTimer >= config.PATROL_TIME || HasEnemyInAttackRange)
        {
            stateTimer = 0f;

            isAttackAfterMove = false;

            ChangeState(E_IDLE_STATE);
        }
    }

    public override void PreAttack()
    {
        if (!IsAttacking)
        {
            SetBool(CharacterState.Attack, true);

            base.PreAttack();

            FacingPlayer();
            ChangeAnim(ChooseCombo());
        }
    }

    private string ChooseCombo()
    {
        int randNum = UnityEngine.Random.Range(0,3);

        switch (randNum)
        {
            case 0:
                currentMagicName = attack_1;
                return S_Constant.ANIM_BOSS_ATTACK_1;
            case 1:
                currentMagicName = attack_2;
                return S_Constant.ANIM_BOSS_ATTACK_2;
            case 2:
                currentMagicName = attack_3;
                return S_Constant.ANIM_BOSS_ATTACK_3;
        }

        currentMagicName = attack_1;
        return S_Constant.ANIM_BOSS_ATTACK_1;
    }

    public override void Attack()
    {
        List<Damageable> targetInRange = attackZone.GetTargetList();
        foreach (Damageable target in targetInRange)
        {
            target.Hit(config.NormalDamage);
        }
    }

    public override void Die()
    {
        IsHitOrHealEvent?.Invoke();

        base.Die();
    }

    public override void Death()
    {
        IsDeathEvent?.Invoke();

        base.Death();
    }

    public override void Hit()
    {
        IsHitOrHealEvent?.Invoke();


        base.Hit();
    }
}
