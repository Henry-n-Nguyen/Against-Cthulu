using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSpawnObj : Magic
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 12f;
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
        Despawn();
    }

    public override void Launch()
    {
        if (!isExist) return;

        timer += Time.deltaTime;

        if (timer >= existTime)
        {
            timer = 0f;
            isExist = false;
            Despawn();
        }

        float distance = speed * Time.deltaTime;
        magicTF.position += magicTF.right * distance;
    }

    public override void Spawn(Transform tf)
    {
        magicTF.position = tf.position;
        magicTF.right = tf.right;

        gameObject.SetActive(true);

        anim.SetTrigger(S_Constant.ANIM_TRIGGER);
        isExist = true;
    }

    public override void Despawn()
    {
        timer = 0f;
        base.Despawn();
    }
}
