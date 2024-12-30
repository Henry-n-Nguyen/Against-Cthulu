using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : AbstractEnemy
{
    [Header("SO Config")]
    [SerializeField] private BossEnemyConfigSO config;

    public event Action IsHitOrHealEvent;
    public event Action IsDeathEvent;

    [field: SerializeField] public Sprite Portrait { get; private set; }
    [field: SerializeField] public string Name { get; private set; }

    private void Awake()
    {
        InitStat();
    }

    public override void OnInit()
    {
        Horizontal = -1;
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

        ChangeState(E_DEAD_STATE);
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
