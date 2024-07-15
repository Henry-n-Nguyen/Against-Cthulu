using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbstractCharacter : MonoBehaviour
{
    // Reference Variables
    [SerializeField] protected AbstractCharacter characterScript;
    [SerializeField] protected Transform characterTransform;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask groundLayer;

    [SerializeField] protected Animator anim;

    // Private Variables
    private IState<AbstractCharacter> currentState;
    private string currentAnimName;

    protected float horizontal;
    protected float speed = 4f;
    protected float jumpingPower = 8f;

    protected bool isAttacked = false;
    protected bool isRunning = false;

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
        isRunning = true;
        ChangeAnim(Constant.TRIGGER_RUN);
    }

    public virtual void Idle()
    {
        isRunning = false;
        ChangeAnim(Constant.TRIGGER_IDLE);
    }

    public void Attack()
    {
        isAttacked = true;
        ChangeState(new AttackState());
    }

    public void Shoot()
    {
        isAttacked = true;
        ChangeState(new ShootState());
    }

    public virtual void ActiveAttack()
    {
        ChangeAnim(Constant.TRIGGER_ATTACK);
    }

    public virtual void ActiveShoot()
    {
        ChangeAnim(Constant.TRIGGER_SHOOT);
    }

    public void ResetAttack()
    {
        if (isRunning) ChangeState(new PatrolState());
        else ChangeState(new IdleState());
    }

    public virtual void Death()
    {
        ChangeAnim(Constant.TRIGGER_DEATH);
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
