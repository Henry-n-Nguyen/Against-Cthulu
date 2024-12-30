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
    public static EExhaustedState E_EXHAUSTED_STATE = new EExhaustedState();

    // private variable
    private IState<AbstractEnemy> currentState;

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
        CheckCliff();

        currentState?.OnExecute(this);
    }

    public override void OnInit()
    {
        ChangeState(E_IDLE_STATE);
    }

    public override void ChangeState<T>(IState<T> state)
    {
        currentState?.OnExit(this);

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

    public virtual void Exhaust() { }

    public override void PreAttack() { }

    public override void Hit()
    {
        SetBool(CharacterState.Hit, true);
        ChangeAnim(S_Constant.ANIM_HIT);
        ChangeState(E_HIT_STATE);
    }

    public override void Die()
    {
        Drop();
        ChangeState(E_DEAD_STATE);
    }

    public override void CallBackState()
    {
        ChangeState(E_IDLE_STATE);
    }

    public void FacingPlayer()
    {
        Transform playerTF = GamePlayManager.Ins.player.characterTF;

        if (playerTF.position.x < characterTF.position.x && Horizontal > 0
            || playerTF.position.x > characterTF.position.x && Horizontal < 0)
        {
            Horizontal = -Horizontal;

            Flip();
        }
    }

    // Check Function
    protected void CheckWall()
    {
        isTouchingWall = Physics2D.OverlapArea(wallCheck.bounds.min, wallCheck.bounds.max, G_Constant.WALL_LAYER);

        if (isTouchingWall)
        {
            isTouchingWall = false;

            Horizontal = -Horizontal;

            Flip();
        }
    }

    protected void CheckCliff()
    {
        if (!IsGrounded)
        {
            Horizontal = -Horizontal;

            Flip();
        }
    }

    // Others
    public virtual void Drop()
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
