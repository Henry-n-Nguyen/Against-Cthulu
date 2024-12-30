using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObjects/Data/BossDataSO")]
public class BossDataSO : ScriptableObject
{
    [field: SerializeField] public List<BossEnemy> bossEnemies { get; private set; }
}
