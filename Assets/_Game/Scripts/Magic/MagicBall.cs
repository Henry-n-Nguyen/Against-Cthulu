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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollideWithPlayer(collision);
    }

    private void CollideWithPlayer(Collider2D col)
    {
        if (!col.CompareTag(S_Constant.TAG_PLAYER)) return;

        Despawn();
    }

    public override void Launch()
    {
        existTime -= Time.deltaTime;

        if (existTime < 0f) Despawn();

        float distance = speed * Time.deltaTime;

        magicTF.position += magicTF.right * distance;
    }

    public override void Spawn(Transform parent)
    {
        ballAnim.SetTrigger(S_Constant.ANIM_TRIGGER);
        
        base.Spawn(parent);
    }

    public override void Despawn()
    {
        ballAnim.SetTrigger(S_Constant.ANIM_DESPAWN);

        base.Despawn();
    }
}
