using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemies : Enemy
{
    // Override Function
    public override void OnInit()
    {
        base.OnInit();
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Idle()
    {
        base.Idle();
    }

    public override void Attack()
    {
        base.Attack();
    }

    public void ActiveAttack()
    {
        ProjectileManager.instance.Spawn(characterScript);
    }

    public override void Death()
    {
        base.Death();
    }
}
