using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBeam : Magic
{
    [SerializeField] private Animator anim;

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
            damageable.Hit(Mathf.RoundToInt(damage * Multiplier));
        }
    }

    public override void CollideWithEnvironment(Collider2D col)
    {
        base.CollideWithEnvironment(col);
    }

    public override void Launch()
    {
        if (!isExist) return;

        timer += Time.deltaTime;

        if (timer >= 0.5f)
        {
            timer = 0f;
            isExist = false;
            anim.SetTrigger(S_Constant.ANIM_DESPAWN);
        }
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
