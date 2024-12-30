using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicInstantAim : Magic
{
    [SerializeField] private Animator anim;

    public override void CollideWithCharacter(Collider2D col)
    {
        base.CollideWithCharacter(col);

        if (col.gameObject.CompareTag(tag)) return;

        Damageable damageable = col.GetComponent<Damageable>();

        if (damageable != null)
        {
            damageable.Hit(Mathf.RoundToInt(damage * Multiplier));
        }
    }

    public override void Launch()
    {

    }

    public override void Spawn(Transform tf)
    {
        Transform playerTF = GamePlayManager.Ins.player.characterTF;

        magicTF.position = playerTF.position;
        magicTF.right = playerTF.right;

        gameObject.SetActive(true);

        anim.SetTrigger(S_Constant.ANIM_TRIGGER);
    }

    public override void Despawn()
    {
        base.Despawn();
    }
}
