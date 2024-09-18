using HuySpace;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbstractCharacter : GameUnit
{
    // Reference Variables
    [Header("Character References")]
    [SerializeField] protected Transform characterTF;
    [SerializeField] protected Animator anim;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Transform shootTF;
    [SerializeField] protected BoxCollider2D groundCheck;

    [Header("Detection Zones")]
    [SerializeField] protected AttackZone attackZone;

    // Private Variables
    private string currentAnimName;

    [field: Header("Basic Stats")]
    [field: SerializeField] public float Horizontal { get; protected set; }
    [field: SerializeField] public float Vertical { get; protected set; }
    [field: SerializeField] public float WalkSpeed { get; protected set; } = 2f;
    [field: SerializeField] public float RunSpeed { get; protected set; } = 4f;
    [field: SerializeField] public float JumpForce { get; protected set; } = 6f;
    [field: SerializeField] public float SlideForce { get; protected set; } = 12f;
    [field: SerializeField] public int NormalDamage { get; protected set; } = 5;
    
    [HideInInspector] public Vector2 RbVelocity { get { return rb.velocity; } protected set { RbVelocity = value; } }

    [field: Header("Boolean")]
    [field: SerializeField] public bool IsGrounded { get; private set; } = false;
    [field: SerializeField] public bool IsRunning { get; private set; } = false;
    [field: SerializeField] public bool IsJumping { get; private set; } = false;
    [field: SerializeField] public bool IsAttacking { get; private set; } = false;
    [field: SerializeField] public bool IsSliding { get; private set; } = false;
    [field: SerializeField] public bool IsHit { get; private set; } = false;

    [Header("Magic,Effect")]
    public Magic prefab;
    public CharacterEffect ghostEffect;
    public CharacterEffect effectSpawned;

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

    public void Flip()
    {
        if (Mathf.Abs(Horizontal) > 0.01f) characterTF.rotation = Quaternion.Euler(new Vector3(0, Horizontal > 0.01f ? 0 : 180, 0));
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

    public virtual void SpawnEffect()
    {
        effectSpawned = Instantiate(ghostEffect, characterTF);
        effectSpawned.Init(this);
        effectSpawned.Spawn(characterTF);
    }

    public virtual void DeSpawnEffect()
    {
        Destroy(effectSpawned.gameObject);
    }

    public void Jump(Vector2 jumpVector)
    {
        rb.velocity = Vector2.up * jumpVector.y * JumpForce + Vector2.right * jumpVector.x;
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
