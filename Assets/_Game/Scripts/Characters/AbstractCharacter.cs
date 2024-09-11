using HuySpace;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbstractCharacter : MonoBehaviour
{
    // Constant
    public static PIdleState IDLE_STATE = new PIdleState();
    public static PMoveState MOVE_STATE = new PMoveState();
    public static POnAirState ON_AIR_STATE = new POnAirState();
    public static PAttackState ATTACK_STATE = new PAttackState();
    public static PCastMagicState CAST_MAGIC_STATE = new PCastMagicState();
    public static PHitState HIT_STATE = new PHitState();
    public static PDeadState DEAD_STATE = new PDeadState();

    // Reference Variables
    [Header("Character References")]
    [SerializeField] protected AbstractCharacter characterScript;
    [SerializeField] protected Transform characterTransform;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform shootTF;
    [SerializeField] protected BoxCollider2D groundCheck;

    [Header("Detection Zones")]
    [SerializeField] protected AttackZone attackZone;

    // Private Variables
    [SerializeField] private IState<AbstractCharacter> currentState;
    private string currentAnimName;

    [field: Header("Basic Stats")]
    [field: SerializeField] public float horizontal { get; protected set; }
    [field: SerializeField] public float vertical { get; protected set; }
    [field: SerializeField] public float walkSpeed { get; protected set; } = 2f;
    [field: SerializeField] public float runSpeed { get; protected set; } = 4f;
    [field: SerializeField] public float jumpForce { get; protected set; } = 6f;
    [field: SerializeField] public int normalDamage { get; protected set; } = 5;
    
    [HideInInspector] public Vector2 rbVelocity { get { return rb.velocity; } protected set { } }

    [field: Header("Boolean")]
    [field: SerializeField] public bool IsGrounded { get; private set; } = false;
    [field: SerializeField] public bool IsRunning { get; private set; } = false;
    [field: SerializeField] public bool IsJumping { get; private set; } = false;
    [field: SerializeField] public bool IsAttacking { get; private set; } = false;

    [Header("Magic")]
    public Magic prefab;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        GatherInput();

        currentState?.OnExecute(this);
    }

    private void GatherInput()
    {
        CheckGround();

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    public virtual void OnInit()
    {
        ChangeState(IDLE_STATE);
    }

    public void ChangeState(IState<AbstractCharacter> state)
    {
        currentState?.OnExit(this);

        currentState = state;

        currentState?.OnEnter(this);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            currentAnimName = animName;
            anim.ResetTrigger(currentAnimName);
            anim.SetTrigger(currentAnimName);
        }
    }

    // Private Function


    // State Function
    public virtual void Idle()
    {
        Flip();

        //ChangeAnim(Constant.ANIM_IDLE);
    }

    public virtual void Move() 
    {
        Flip();

        ChangeAnim(S_Constant.ANIM_RUN_LOOP);
    }

    public virtual void OnAir()
    {
        Flip();
    }

    public virtual void PreAttack() { }

    public virtual void CastMagic() 
    {
        Magic magic = Instantiate(prefab);
        magic.Spawn(shootTF);
    }

    public virtual void Death()
    {
        ChangeAnim(S_Constant.ANIM_DEATH);
    }

    public virtual void Hit()
    {
        anim.SetTrigger(S_Constant.ANIM_HIT);
        ChangeState(HIT_STATE);
    }

    public void Flip()
    {
        if (Mathf.Abs(horizontal) > 0.01f) characterTransform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0.01f ? 0 : 180, 0));
    }

    public virtual void Attack()
    {
        List<Damageable> targetInRange = attackZone.GetTargetList();
        foreach (Damageable target in targetInRange)
        {
            target.Hit(normalDamage);
        }
    }

    public void Jump(Vector2 jumpVector)
    {
        rb.AddForce(jumpVector * jumpForce, ForceMode2D.Impulse);
    }

    public virtual void Die()
    {
        ChangeState(DEAD_STATE);
    }

    public void SetMove(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetBool(CharacterState state, bool value)
    {
        switch (state)
        {
            case CharacterState.Run:
                IsRunning = value;
                break;
            case CharacterState.Attack:
                IsAttacking = value;
                break;
            case CharacterState.Jump:
                IsJumping = value;
                break;
            default: break;
        }
    }

    // Protected Function
    protected void CheckGround()
    {
        IsGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, G_Constant.GROUND_LAYER).Length > 0;
    }
}
