using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private AbstractCharacter character;

    [Header("Boolean")]
    [SerializeField] private bool isInvincible;

    // Is Alive
    [SerializeField] private bool _isAlive = true;

    public bool IsAlive 
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
        }
    }

    [Header("Float")]
    [SerializeField] private float timer = 0f;
    [SerializeField] private float timer1 = 0f;

    public float invincibleTime { get; private set; } = 1f;

    // Max HP
    [SerializeField] private float _maxHP = 100f;

    public float MaxHP
    {
        get
        {
            return _maxHP;
        }
        set 
        {
            _maxHP = value;
        }
    }

    // HP
    [SerializeField] private float _HP = 100f;

    public float HP
    {
        get
        {
            return _HP;
        }
        set
        {
            _HP = value;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Hit(0);
        }

        CheckInvincibleState();
    }

    public void Hit(int damage)
    {
        if (IsAlive && !isInvincible)
        {
            _HP -= damage;
            isInvincible = true;

            // If HP drops below 0, character is no longer alive
            if (_HP <= 0)
            {
                IsAlive = false;
                character.CallBackState();
                character.Die();
            }
            else
            {
                if (!character.IsAttacking) character.Hit();
            }
        }
    }

    private void CheckInvincibleState()
    {
        if (isInvincible)
        {
            if (timer > invincibleTime)
            {
                isInvincible = false;
                timer = 0;
            }

            timer += Time.deltaTime;
        }
    }
}
