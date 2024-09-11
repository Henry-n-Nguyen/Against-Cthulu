using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractEnemy : AbstractCharacter
{
    // Constant
    public static EIdleState E_IDLE_STATE = new EIdleState();
    public static EMoveState E_MOVE_STATE = new EMoveState();
    public static EAttackState E_ATTACK_STATE = new EAttackState();
    public static EHitState E_HIT_STATE = new EHitState();
    public static EDeadState E_DEAD_STATE = new EDeadState();

    // Reference Variables
    [Header("Character References")]
    [SerializeField] protected BoxCollider2D wallCheck;

    [Header("Detection Zones")]
    [SerializeField] protected DetectionZone detectZone;

    public bool DetectedTarget { get { return detectZone.HasTargetInRange; } protected set { } }
    public bool HasEnemyInAttackRange { get { return attackZone.HasTargetInRange; } protected set { } }

    // Private Variables
    private IState<AbstractEnemy> currentState;

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

    public void ChangeState(IState<AbstractEnemy> state)
    {
        currentState?.OnExit(this);

        currentState = state;

        currentState?.OnEnter(this);
    }

    public override void Hit()
    {
        anim.SetTrigger(S_Constant.ANIM_HIT);
        ChangeState(E_HIT_STATE);
    }

    public override void Die()
    {
        ChangeState(E_DEAD_STATE);
    }

    // Check Function
    protected void CheckWall()
    {
        isTouchingWall = Physics2D.OverlapAreaAll(wallCheck.bounds.min, wallCheck.bounds.max, G_Constant.WALL_LAYER).Length > 0;

        if (isTouchingWall)
        {
            isTouchingWall = false;

            horizontal = -horizontal;

            Flip();
        }
    }
}
