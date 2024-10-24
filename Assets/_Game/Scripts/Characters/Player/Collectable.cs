using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : GameUnit
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(S_Constant.TAG_PLAYER))
        {
            Despawn();
        }
    }

    private void Despawn()
    {
        SimplePool.Despawn(this);
    }
}
