using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MagicBall : Magic
{
    [SerializeField] private Animator ballAnim;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float existTime = 2.5f;

    private bool isExist = true;

    public override void CollideWithCharacter(Collider2D col)
    {
        base.CollideWithCharacter(col);

        Damageable damageable = col.GetComponent<Damageable>();

        if (damageable != null)
        {
            damageable.Hit(damage);
        }

        isExist = false;
        ballAnim.SetTrigger(S_Constant.ANIM_DESPAWN);
    }

    public override void CollideWithEnvironment(Collider2D col)
    {
        base.CollideWithEnvironment(col);

        isExist = false;
        ballAnim.SetTrigger(S_Constant.ANIM_DESPAWN);
    }

    public override void Launch()
    {
        if (!isExist) return;

        existTime -= Time.deltaTime;

        if (existTime <= 0f)
        {
            isExist = false;
            ballAnim.SetTrigger(S_Constant.ANIM_DESPAWN);
        }

        float distance = speed * Time.deltaTime;
        magicTF.position += magicTF.right * distance;
    }

    public override void Spawn(Transform tf)
    {
        ballAnim.SetTrigger(S_Constant.ANIM_TRIGGER);
        
        base.Spawn(tf);
    }

    public override void Despawn()
    {
        base.Despawn();
    }
}
