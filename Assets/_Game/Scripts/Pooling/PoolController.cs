using System.Collections.Generic;
using UnityEngine;
using HuySpace;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;
using JetBrains.Annotations;

public class PoolController : MonoBehaviour
{
    [Header("Pool")]
    public List<PoolAmountGroup> poolGroups;

    void Awake()
    {
        foreach (PoolAmountGroup poolGroup in poolGroups) {
            for (int i = 0; i < poolGroup.poolWithRoot.Count; i++)
            {
                SimplePool.Preload(poolGroup.poolWithRoot[i].prefab, poolGroup.poolWithRoot[i].amount, poolGroup.poolWithRoot[i].root);
            }
        }
    }
}

[System.Serializable]
public class PoolAmountGroup
{
    public string name;
    public List<PoolAmount> poolWithRoot;
}

[System.Serializable]
public class PoolAmount
{
    [Header("-- Pool Amount --")]
    public Transform root;
    public GameUnit prefab;
    public int amount;

    public PoolAmount(Transform root, GameUnit prefab, int amount)
    {
        this.root = root;
        this.prefab = prefab;
        this.amount = amount;
    }
}
