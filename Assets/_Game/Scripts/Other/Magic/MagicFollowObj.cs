using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFollowObj : Magic
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float existTime = 2.5f;

    private float timer = 0f;

    private bool isExist = true;
    private bool needTimeToDmg = true;

    public override void CollideWithCharacter(Collider2D col)
    {
        base.CollideWithCharacter(col);

        if (col.gameObject.CompareTag(tag)) return;

        Damageable damageable = col.GetComponent<Damageable>();

        if (!damageable.IsAlive) return;

        if (damageable != null)
        {
            if (needTimeToDmg) { needTimeToDmg = false; }
            else damageable.Hit(Mathf.RoundToInt(damage * Multiplier));
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
            anim.SetTrigger(S_Constant.ANIM_DESPAWN);
        }

        Vector3 followVec = (GamePlayManager.Ins.player.characterTF.position - magicTF.position).normalized;
        float distance = speed * Time.deltaTime;

        if (followVec.x >= 0f) magicTF.rotation = Quaternion.Euler(Vector3.up * 180f);
        else magicTF.rotation = Quaternion.Euler(Vector3.zero);

        magicTF.position += followVec * distance;
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
        needTimeToDmg = true;
        base.Despawn();
    }
}
