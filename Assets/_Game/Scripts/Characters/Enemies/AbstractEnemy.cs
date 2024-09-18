using HuySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEnemy : AbstractCharacter
{
    // Constant
    public static EIdleState E_IDLE_STATE = new EIdleState();
    public static EMoveState E_MOVE_STATE = new EMoveState();
    public static EAttackState E_ATTACK_STATE = new EAttackState();
    public static ECastMagicState E_CAST_MAGIC_STATE = new ECastMagicState();
    public static EHitState E_HIT_STATE = new EHitState();
    public static EDeadState E_DEAD_STATE = new EDeadState();

    // private variable
    private IState<AbstractEnemy> currentState;
    private IState<AbstractEnemy> prevState;

    // Reference Variables
    [Header("Character References")]
    [SerializeField] protected BoxCollider2D wallCheck;

    [Header("Detection Zones")]
    [SerializeField] protected DetectionZone detectZone;

    public bool DetectedTarget { get { return detectZone.HasTargetInRange; } protected set { } }
    public bool HasEnemyInAttackRange { get { return attackZone.HasTargetInRange; } protected set { } }

    [field: Header("Boolean")]
    [field: SerializeField] public bool isTouchingWall { get; protected set; } = false;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        CheckGround();
        CheckWall();

        currentState?.OnExecute(this);
    }

    public override void OnInit()
    {
        ChangeState(E_IDLE_STATE);
    }

    public override void ChangeState<T>(IState<T> state)
    {
        currentState?.OnExit(this);

        if (currentState != null) prevState = currentState;

        currentState = state as IState<AbstractEnemy>;

        currentState?.OnEnter(this);
    }

    // State Function
    public override void Idle()
    {
        Flip();
        ChangeAnim(S_Constant.ANIM_IDLE);
    }

    public override void Move()
    {
        Flip();
        ChangeAnim(S_Constant.ANIM_RUN);
    }

    public override void PreAttack() { }

    public override void Hit()
    {
        SetBool(CharacterState.Hit, true);
        ChangeState(E_HIT_STATE);
    }

    public override void Die()
    {
        ChangeState(E_DEAD_STATE);
    }

    public override void CallBackState()
    {
        ChangeState(prevState);
    }

    // Check Function
    protected void CheckWall()
    {
        isTouchingWall = Physics2D.OverlapAreaAll(wallCheck.bounds.min, wallCheck.bounds.max, G_Constant.WALL_LAYER).Length > 0;

        if (isTouchingWall)
        {
            isTouchingWall = false;

            Horizontal = -Horizontal;

            Flip();

            SetMove(characterTF.right * WalkSpeed + Vector3.up * RbVelocity.y);
        }
    }

    protected void CheckCliff()
    {
        // TEST
    }
}
