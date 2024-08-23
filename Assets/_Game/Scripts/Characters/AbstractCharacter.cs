using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbstractCharacter : MonoBehaviour
{
    // Constant
    public static IdleState IDLE_STATE = new IdleState();
    public static MoveState MOVE_STATE = new MoveState();
    public static OnAirState ON_AIR_STATE = new OnAirState();
    public static AttackState ATTACK_STATE = new AttackState();
    public static HitState HIT_STATE = new HitState();
    public static DeadState DEAD_STATE = new DeadState();

    // Reference Variables
    [SerializeField] public AbstractCharacter characterScript;
    [SerializeField] public Transform characterTransform;

    [SerializeField] protected BoxCollider2D groundCheck;
    [SerializeField] protected LayerMask groundLayer;

    public Animator anim;
    public Rigidbody2D rb;

    // Private Variables
    private IState<AbstractCharacter> currentState;
    private string currentAnimName;

    [field: SerializeField] public float horizontal { get; protected set; }
    [field: SerializeField] public float vertical { get; protected set; }
    [field: SerializeField] public float walkSpeed { get; protected set; } = 2f;
    [field: SerializeField] public float runSpeed { get; protected set; } = 4f;
    [field: SerializeField] public float jumpForce { get; protected set; } = 6f;

    public bool isGrounded = false;
    public bool isRunning = false;
    public bool isJumping = false;
    public bool isAttacking = false;

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

        isAttacking = false;
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
            anim.ResetTrigger(animName);
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

        ChangeAnim(Constant.ANIM_RUN_LOOP);
    }

    public virtual void OnAir()
    {
        Flip();
    }

    public virtual void Attack() { }

    public virtual void Death()
    {
        ChangeAnim(Constant.ANIM_DEATH);
    }

    public virtual void Hit()
    {
        ChangeAnim(Constant.ANIM_HIT);
    }
    public void Flip()
    {
        if (Mathf.Abs(horizontal) > 0.01f) characterTransform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0.01f ? 0 : 180, 0));
    }

    // Protected Function
    protected void CheckGround()
    {
        isGrounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundLayer).Length > 0;
    }
}
