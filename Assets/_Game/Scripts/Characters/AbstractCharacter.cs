using HuySpace;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class AbstractCharacter : GameUnit
{
    // Reference Variables
    [Header("Character References")]
    [SerializeField] public Transform characterTF;

    [SerializeField] protected BoxCollider2D characterCollide;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform shootTF;
    [SerializeField] protected BoxCollider2D groundCheck;

    [Header("Detection Zones")]
    [SerializeField] protected AttackZone attackZone;

    // Private Variables
    private string currentAnimName;
    private bool facingRight = true;
    private GameObject currentFloatingPlatform;

    // Bool Variables
    [field: Header("Basic Stats")]
    [field: SerializeField] public float Horizontal { get; protected set; }
    [field: SerializeField] public float Vertical { get; protected set; }
    [field: SerializeField] public float WalkSpeed { get; protected set; } = 2f;
    [field: SerializeField] public float RunSpeed { get; protected set; } = 4f;
    [field: SerializeField] public float JumpForce { get; protected set; } = 6f;
    [field: SerializeField] public float SlideForce { get; protected set; } = 12f;
    [field: SerializeField] public int NormalDamage { get; protected set; } = 5;
    
    [HideInInspector] public Vector2 RbVelocity { get { return rb.velocity; } protected set { RbVelocity = value; } }

    [field: Header("Boolean For Check")]
    [field: SerializeField] public bool IsGrounded { get; private set; } = false;
    [field: SerializeField] public bool IsRunning { get; private set; } = false;
    [field: SerializeField] public bool IsJumping { get; private set; } = false;
    [field: SerializeField] public bool IsDoubleJumping { get; private set; } = false;
    [field: SerializeField] public bool IsAttacking { get; private set; } = false;
    [field: SerializeField] public bool IsSliding { get; private set; } = false;
    [field: SerializeField] public bool IsHit { get; private set; } = false;

    [Header("Magic")]
    public Magic prefab;

    void Start()
    {
        OnInit();
    }

    public virtual void OnInit() { }

    public virtual void ChangeState<T>(IState<T> state) where T : AbstractCharacter { }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            currentAnimName = animName;
            anim.ResetTrigger(currentAnimName);
            anim.SetTrigger(currentAnimName);
        }
    }

    public void ChangeAnimDirectly(string animName)
    {
        currentAnimName = animName;
        anim.ResetTrigger(currentAnimName);
        anim.SetTrigger(currentAnimName);
    }

    // Collider Function
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(S_Constant.TAG_FLOATING))
        {
            currentFloatingPlatform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(S_Constant.TAG_FLOATING))
        {
            currentFloatingPlatform = null;
        }
    }

    protected IEnumerator DisableCollision()
    {
        CompositeCollider2D platformCollide = currentFloatingPlatform.GetComponent<CompositeCollider2D>();

        Physics2D.IgnoreCollision(characterCollide, platformCollide);
        yield return new WaitForSeconds(0.75f);
        Physics2D.IgnoreCollision(characterCollide, platformCollide, false);
    }

    // State Function
    public virtual void Idle() { }

    public virtual void Move() { }

    public virtual void OnAir() { }

    public virtual void PreAttack() { }

    public virtual void Death()
    {
        SetMove(Vector2.zero + Vector2.up * RbVelocity.y);
        ChangeAnim(S_Constant.ANIM_DEATH);
    }

    public virtual void Hit() { }

    public void CheckIfShouldFlip()
    {
        if (Mathf.Abs(Horizontal) < 0.01f) return;
        if ((Horizontal > 0.01f && !facingRight))
        {
            facingRight = true;
            Flip();
        }
        else if ((Horizontal < -0.01f && facingRight))
        {
            facingRight = false;
            Flip();
        }
    }

    public virtual void Flip()
    {
        characterTF.rotation = Quaternion.Euler(new Vector3(0, Horizontal > 0.01f ? 0 : 180, 0));
    }

    public virtual void Attack()
    {
        List<Damageable> targetInRange = attackZone.GetTargetList();
        foreach (Damageable target in targetInRange)
        {
            target.Hit(NormalDamage);
        }
    }

    public virtual void CastMagic() 
    {
        Magic magic = Instantiate(prefab);
        magic.Init(this);
        magic.Spawn(shootTF);
    }
    public void Jump(Vector2 jumpVector)
    {
        rb.velocity = Vector2.up * jumpVector.y * JumpForce + Vector2.right * jumpVector.x;
    }

    public void FallFromPlatform()
    {
        if (currentFloatingPlatform != null)
        {
            StartCoroutine(DisableCollision());
        }
    }

    public virtual void Die() { }

    public virtual void CallBackState() { }

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
            case CharacterState.DoubleJump:
                IsDoubleJumping = value;
                break;
            case CharacterState.Hit:
                IsHit = value;
                break;
            case CharacterState.Slide:
                IsSliding = value;
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
