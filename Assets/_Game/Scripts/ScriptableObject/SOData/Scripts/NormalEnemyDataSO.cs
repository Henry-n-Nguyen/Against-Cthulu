using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObjects/Data/NormalEnemyDataSO")]
public class NormalEnemyDataSO : ScriptableObject
{
    [field: SerializeField] public List<NormalEnemy> norEnemies { get; private set; }

    public NormalEnemy GetRandomEnemies()
    {
        int randInt = Random.Range(0, norEnemies.Count);

        return norEnemies[randInt];
    }
}
