using UnityEngine;
using System.Collections.Generic;
using System;
using HuySpace;

public static class SimplePool
{
    class Pool
    {
        //parent contain all pool member
        Transform m_root = null;
        //object is can collect back to pool
        bool m_collect;
        //list object in pool
        Queue<GameUnit> m_inactive;
        //collect obj active ingame
        HashSet<GameUnit> m_active;
        // The prefab that we are pooling
        GameUnit m_prefab;

        public bool IsCollect { get => m_collect; }
        public HashSet<GameUnit> Active => m_active;
        public int Count => m_inactive.Count + m_active.Count;
        public Transform Root => m_root;

        // Constructor
        public Pool(GameUnit prefab, int initialQty, Transform parent, bool collect)
        {
            m_inactive = new Queue<GameUnit>(initialQty);
            m_root = parent;
            this.m_prefab = prefab;
            m_collect = collect;
            if (m_collect) m_active = new HashSet<GameUnit>();
        }

        // Spawn an object from our pool with position and rotation
        public GameUnit Spawn(Vector3 pos, Quaternion rot)
        {
            GameUnit obj = Spawn();

            obj.TF.SetPositionAndRotation(pos, rot);

            return obj;
        }

        //spawn gameunit
        public GameUnit Spawn()
        {
            GameUnit obj;
            if (m_inactive.Count == 0)
            {
                obj = (GameUnit)GameObject.Instantiate(m_prefab, m_root);
            }
            else
            {
                // Grab the last object in the inactive array
                obj = m_inactive.Dequeue();

                if (obj == null)
                {
                    return Spawn();
                }
            }

            if (m_collect) m_active.Add(obj);

            obj.gameObject.SetActive(true);

            return obj;
        }

        // Return an object to the inactive pool.
        public void Despawn(GameUnit obj)
        {
            if (obj != null /*&& !inactive.Contains(obj)*/)
            {
                obj.gameObject.SetActive(false);
                m_inactive.Enqueue(obj);

                if (memberInParent.Contains(obj.GetInstanceID()))
                {
                    obj.TF.SetParent(GetPool(obj).Root);
                    memberInParent.Remove(obj.GetInstanceID());
                }
            }

            if (m_collect) m_active.Remove(obj);
        }

        //destroy all unit in pool
        public void Release()
        {
            while (m_inactive.Count > 0)
            {
                GameUnit go = m_inactive.Dequeue();
                GameObject.DestroyImmediate(go);
            }
            m_inactive.Clear();
        }

        //collect all unit return to pool
        public void Collect()
        {
            foreach (var item in m_active)
            {
                Despawn(item);
            }
        }
    }

    public const int DEFAULT_POOL_SIZE = 3;

    //dictionary for faster search from pool type to prefab
    static Dictionary<PoolType, GameUnit> poolTypes = new Dictionary<PoolType, GameUnit>();

    //save unit that is child transform other object
    static HashSet<int> memberInParent = new HashSet<int>();

    private static Transform root;

    public static Transform Root
    {
        get
        {
            if (root == null)
            {
                PoolController controler = GameObject.FindObjectOfType<PoolController>();
                root = controler != null ? controler.transform : new GameObject("Pool").transform;
            }

            return root;
        }
    }

    // All of our pools
    static Dictionary<PoolType, Pool> poolInstance = new Dictionary<PoolType, Pool>();

    // preload object and pool
    static public void Preload(GameUnit prefab, int qty = 1, Transform parent = null, bool collect = false)
    {
        if (!poolTypes.ContainsKey(prefab.poolType))
        {
            poolTypes.Add(prefab.poolType, prefab);
        }

        if (prefab == null)
        {
            Debug.LogError(parent.name + " : IS EMPTY!!!");
            return;
        }

        InitPool(prefab, qty, parent, collect);

        // Make an array to grab the objects we're about to pre-spawn.
        GameUnit[] obs = new GameUnit[qty];
        for (int i = 0; i < qty; i++)
        {
            obs[i] = Spawn(prefab);
        }

        // Now despawn them all.
        for (int i = 0; i < qty; i++)
        {
            Despawn(obs[i]);
        }
    }

    // init pool
    static void InitPool(GameUnit prefab = null, int qty = DEFAULT_POOL_SIZE, Transform parent = null, bool collect = false)
    {
        if (prefab != null && !IsHasPool(prefab))
        {
            poolInstance.Add(prefab.poolType, new Pool(prefab, qty, parent, collect));
        }
    }
    static private bool IsHasPool(GameUnit obj)
    {
        return poolInstance.ContainsKey(obj.poolType);
    }
    static private Pool GetPool(GameUnit obj)
    {
        return poolInstance[obj.poolType];
    }
    public static GameUnit GetPrefabByType(PoolType poolType)
    {
        if (!poolTypes.ContainsKey(poolType) || poolTypes[poolType] == null)
        {
            GameUnit[] resources = Resources.LoadAll<GameUnit>("Pool");

            for (int i = 0; i < resources.Length; i++)
            {
                poolTypes[resources[i].poolType] = resources[i];
            }
        }

        return poolTypes[poolType];
    }

    #region Get List object ACTIVE
    // get all member is active in game
    public static HashSet<GameUnit> GetAllUnitIsActive(GameUnit obj)
    {
        return IsHasPool(obj) ? GetPool(obj).Active : new HashSet<GameUnit>();
    }
    public static HashSet<GameUnit> GetAllUnitIsActive(PoolType poolType)
    {
        return GetAllUnitIsActive(GetPrefabByType(poolType));
    }  

    #endregion

    #region Spawn
    // Spawn Unit to use
    static public T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rot) where T : GameUnit
    {
        return Spawn(GetPrefabByType(poolType), pos, rot) as T;
    }

    static public T Spawn<T>(PoolType poolType) where T : GameUnit
    {
        return Spawn<T>(GetPrefabByType(poolType));
    }

    static public T Spawn<T>(GameUnit obj) where T : GameUnit
    {
        return Spawn(obj) as T;
    }

    // spawn gameunit with transform parent
    static public T Spawn<T>(GameUnit obj, Transform parent) where T : GameUnit
    {
        return Spawn<T>(obj, obj.TF.localPosition, obj.TF.localRotation, obj.TF.localScale, parent);
    }

    static public T Spawn<T>(GameUnit obj, Vector3 localPoint, Quaternion localRot, Vector3 localScale, Transform parent) where T : GameUnit
    {
        T unit = Spawn<T>(obj);
        unit.TF.SetParent(parent);
        unit.TF.localPosition = localPoint;
        unit.TF.localRotation = localRot;
        unit.TF.localScale = localScale;
        memberInParent.Add(unit.GetInstanceID());
        return unit;
    }

    static public T Spawn<T> (PoolType poolType, Transform parent) where T : GameUnit
    {
        return Spawn<T>(GetPrefabByType(poolType), parent);
    }

    static public GameUnit Spawn(GameUnit obj, Vector3 pos, Quaternion rot)
    {
        if (!IsHasPool(obj))
        {
            Transform newRoot = new GameObject(obj.name).transform;
            newRoot.SetParent(Root);
            Preload(obj, 1, newRoot, true);
        }

        return GetPool(obj).Spawn(pos, rot);
    }
    static public GameUnit Spawn(GameUnit obj)
    {
        if (!IsHasPool(obj))
        {
            Transform newRoot = new GameObject(obj.name).transform;
            newRoot.SetParent(Root);
            Preload(obj, 1, newRoot, true);
        }

        return GetPool(obj).Spawn();
    }
    #endregion

    #region Despawn
    //take gameunit to pool
    static public void Despawn(GameUnit obj)
    {
        if (obj.gameObject.activeSelf)
        {
            if (IsHasPool(obj))
            {
                GetPool(obj).Despawn(obj);
            }
            else
            {
                GameObject.Destroy(obj.gameObject);
            }
        }
    }
    #endregion

    #region Collect
    //collect all pool member comeback to pool
    static public void Collect(GameUnit obj)
    {
        if (IsHasPool(obj)) GetPool(obj).Collect();
    }
    static public void Collect(PoolType poolType)
    {
        Collect(GetPrefabByType(poolType));
    }

    //COLLECT ALL POOL
    static public void CollectAll()
    {
        foreach (var item in poolInstance)
        {
            if (item.Value.IsCollect)
            {
                item.Value.Collect();
            }
        }
    }
    #endregion
}
