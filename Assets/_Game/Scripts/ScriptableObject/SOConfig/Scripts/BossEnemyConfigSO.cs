using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObjects/Config/BossEnemyConfigSO")]
public class BossEnemyConfigSO : ScriptableObject
{
    [field: Header("UI Configs")]
    [field: SerializeField] public Sprite Portrait { get; private set; }
    [field: SerializeField] public string Name { get; private set; }


    [field: Header("General Configs")]
    [field: SerializeField] public int HP { get; private set; } = 100;
    [field: SerializeField] public float Speed { get; private set; } = 2f;
    [field: SerializeField] public float JumpForce { get; protected set; } = 6f;
    [field: SerializeField] public int NormalDamage { get; private set; } = 5;

    [field: Header("Unique Configs")]
    [field: SerializeField] public float IDLE_TIME { get; private set; } = 1.5f;
    [field: SerializeField] public float PATROL_TIME { get; private set; } = 2.5f;
    [field: SerializeField] public float ATTACK_COOLDOWN_TIME { get; private set; } = 1f;
}
