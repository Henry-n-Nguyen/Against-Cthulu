using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObjects/Config/NormalEnemySOConfig")]
public class NormalEnemySOConfig : ScriptableObject
{
    [field: SerializeField] public float IDLE_TIME { get; private set; } = 1.5f;
    [field: SerializeField] public float PATROL_TIME { get; private set; } = 2.5f;
    [field: SerializeField] public float ATTACK_COOLDOWN_TIME { get; private set; } = 1f;
}
