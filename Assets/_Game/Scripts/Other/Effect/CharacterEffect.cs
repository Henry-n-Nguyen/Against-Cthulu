using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEffect : GameUnit
{
    [SerializeField] protected AbstractCharacter owner;
    [SerializeField] protected Transform effectTF;

    public void Init(AbstractCharacter caster)
    {
        owner = caster;
    }

    public virtual void Spawn(Transform tf)
    {
        effectTF.position = tf.position;
    }

    public virtual void Despawn()
    {
        SimplePool.Despawn(this);
    }
}
