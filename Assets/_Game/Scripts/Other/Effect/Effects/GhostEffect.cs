using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : CharacterEffect
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] protected Animator effectAnim;

    private void SetSprite()
    {
        Sprite sprite = owner.GetComponent<SpriteRenderer>().sprite;
        spriteRenderer.sprite = sprite;
    }

    public override void Spawn(Transform tf)
    {
        SetSprite();

        effectAnim.SetTrigger(S_Constant.ANIM_TRIGGER);

        effectTF.rotation = tf.rotation;

        effectTF.position = tf.position - owner.transform.right * 0.2f;
    }

    public override void Despawn()
    {
        SimplePool.Despawn(this);
    }
}
