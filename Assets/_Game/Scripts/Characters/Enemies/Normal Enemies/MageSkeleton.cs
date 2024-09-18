using HuySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSkeleton : AbstractEnemy
{
    [Header("SO Config")]
    [SerializeField] private NormalEnemySOConfig config;

    private float stateTimer;
    private float attackCooldown = 0f;

    // Override Function
    public override void Idle()
    {
        base.Idle();

        SetMove(Vector2.zero + Vector2.up * RbVelocity.y);

        stateTimer += Time.deltaTime;
        attackCooldown += Time.deltaTime;

        if (HasEnemyInAttackRange && attackCooldown > config.ATTACK_COOLDOWN_TIME)
        {
            stateTimer = 0f;

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

            Horizontal = Random.Range(0, 2) == 0 ? 1f : -1f;

            SetBool(CharacterState.Run, true);

            ChangeState(E_MOVE_STATE);
        }
    }

    public override void Move()
    {
        base.Move();

        SetMove(characterTF.right * WalkSpeed + Vector3.up * RbVelocity.y);

        stateTimer += Time.deltaTime;
        attackCooldown += Time.deltaTime;

        if (HasEnemyInAttackRange && attackCooldown > config.ATTACK_COOLDOWN_TIME)
        {
            stateTimer = 0f;

            SetMove(Vector2.zero + Vector2.up * RbVelocity.y);

            ChangeState(E_ATTACK_STATE);
        }
        else if (stateTimer >= config.PATROL_TIME)
        {
            stateTimer = 0f;

            ChangeState(E_IDLE_STATE);
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

    public override void Death()
    {
        base.Death();
    }

    public override void Hit()
    {
        base.Hit();
    }
}
