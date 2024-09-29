using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MagicBall : Magic
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float existTime = 2.5f;

    private float timer = 0f;

    private bool isExist = true;

    public override void CollideWithCharacter(Collider2D col)
    {
        base.CollideWithCharacter(col);

        if (col.gameObject.CompareTag(tag)) return;

        Damageable damageable = col.GetComponent<Damageable>();

        if (!damageable.IsAlive) return;

        if (damageable != null)
        {
            damageable.Hit(damage);
        }

        isExist = false;
        anim.SetTrigger(S_Constant.ANIM_DESPAWN);
    }

    public override void CollideWithEnvironment(Collider2D col)
    {
        base.CollideWithEnvironment(col);

        isExist = false;
        anim.SetTrigger(S_Constant.ANIM_DESPAWN);
    }

    public override void Launch()
    {
        if (!isExist) return;

        timer += Time.deltaTime;

        if (timer >= existTime)
        {
            timer = 0f;
            isExist = false;
            anim.SetTrigger(S_Constant.ANIM_DESPAWN);
        }

        float distance = speed * Time.deltaTime;
        magicTF.position += magicTF.right * distance;
    }

    public override void Spawn(Transform tf)
    {
        anim.SetTrigger(S_Constant.ANIM_TRIGGER);
        isExist = true;
        base.Spawn(tf);
    }

    public override void Despawn()
    {
        timer = 0f;
        base.Despawn();
    }
}
