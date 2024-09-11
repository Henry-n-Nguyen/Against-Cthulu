using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{
    [SerializeField] protected GameObject magic;
    [SerializeField] protected Transform magicTF;

    private void Update()
    {
        Launch();
    }

    public virtual void Launch()
    {

    }

    public virtual void Spawn(Transform parent)
    {
        magicTF.position = parent.position;
        magicTF.right = parent.right;
        magic.SetActive(true);
    }

    public virtual void Despawn()
    {
        magic.SetActive(false);
    }
}
