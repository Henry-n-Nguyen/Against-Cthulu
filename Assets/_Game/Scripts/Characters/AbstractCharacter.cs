using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbstractCharacter : MonoBehaviour
{
    // Reference Variables
    [SerializeField] public AbstractCharacter characterScript;
    [SerializeField] public Transform characterTransform;

    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;

    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;

    // Private Variables
    private IState<AbstractCharacter> currentState;
    private string currentAnimName;

    protected float horizontal;
    protected float vertical;

    protected float walkSpeed = 2f;
    protected float runSpeed = 4f;
    protected float jumpingPower = 5f;

    protected bool isAttacked = false;
    protected bool isRunning = false;
    protected bool isJumping = false;

    //Public Variables
    [Space(5f)]
    [Header("public")]
    public int id;
    public Magic magic;
    public Transform shootPoint;
    public Transform target;

    void Start()
    {
        OnInit();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    public virtual void OnInit()
    {
        horizontal = 0f;

        vertical = 0f;

        target = GamePlayManager.instance.player.characterTransform;

        ChangeState(new IdleState());
    }

    public void ChangeState(IState<AbstractCharacter> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }

    protected void ChangeAnim(string animName)
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
    public virtual void Move()
    {

    }

    public virtual void Idle()
    {

    }

    public virtual void Jump()
    {

    }

    public virtual void Attack()
    {

    }

    public virtual void ResetState()
    {
        if (isRunning) ChangeState(new MoveState());
        else if (isJumping) ChangeState(new JumpState());
        else ChangeState(new IdleState());

        isAttacked = false;
    }

    public virtual void Death()
    {
        ChangeAnim(Constant.TRIGGER_DEATH);
    }

    public virtual void Hit()
    {
        ChangeAnim(Constant.TRIGGER_HIT);
    }

    // Protected Function
    protected bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    protected void Flip()
    {
        if (Mathf.Abs(horizontal) > 0.1f) characterTransform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0.1f ? 0 : 180, 0));
    }

}
