using HuySpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : GameUnit
{
    [SerializeField] protected AbstractCharacter owner;
    [SerializeField] protected Transform magicTF;
    [SerializeField] protected int damage;

    private void Update()
    {
        Launch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(owner.tag)) return;

        CollideWithCharacter(collision);
        CollideWithEnvironment(collision);
    }

    public virtual void CollideWithCharacter(Collider2D col)
    {
        if (!col.CompareTag(S_Constant.TAG_PLAYER) && !col.CompareTag(S_Constant.TAG_ENEMY)) return;
    }

    public virtual void CollideWithEnvironment(Collider2D col)
    {
        //if (!col.CompareTag(S_Constant.TAG_GROUND) && !col.CompareTag(S_Constant.TAG_FLOATING)) return;
        if (!col.CompareTag(S_Constant.TAG_GROUND)) return;
    }

    public void Init(AbstractCharacter caster)
    {
        owner = caster;
        damage = owner.NormalDamage;
    }

    public virtual void Launch()
    {

    }

    public virtual void Spawn(Transform tf)
    {
        magicTF.position = tf.position;
        magicTF.right = tf.right;

        gameObject.SetActive(true);
    }

    public virtual void Despawn()
    {
        Destroy(gameObject);
    }
}
