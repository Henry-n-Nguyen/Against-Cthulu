using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustEffect : CharacterEffect
{
    [SerializeField] private ParticleSystem dust;
    [SerializeField] protected Animator effectAnim;

    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            timer = 0f;
            Despawn();
        }
    }

    public override void Spawn(Transform tf)
    {
        effectTF.position = tf.position - tf.right * 0.2f - Vector3.up * 0.9f;
        effectTF.localScale = Vector3.one * (Mathf.Abs(tf.rotation.y) > 0.01f ? -1f : 1f); 
        dust.Play();
    }

    public override void Despawn()
    {
        SimplePool.Despawn(this);
    }
}
