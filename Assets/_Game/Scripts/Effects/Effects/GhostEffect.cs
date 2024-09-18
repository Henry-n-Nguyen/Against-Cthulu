using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : CharacterEffect
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void SetSprite()
    {
        Sprite sprite = owner.GetComponent<SpriteRenderer>().sprite;
        spriteRenderer.sprite = sprite;
    }

    public override void Spawn(Transform tf)
    {
        SetSprite();

        effectAnim.SetTrigger(S_Constant.ANIM_TRIGGER);

        effectTF.position = tf.position - owner.transform.right * 0.2f;
        gameObject.SetActive(true);
    }

    public override void Despawn()
    {
        Destroy(gameObject);
    }
}
