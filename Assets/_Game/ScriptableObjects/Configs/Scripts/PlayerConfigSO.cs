using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObjects/Config/PlayerConfigSO")]
public class PlayerConfigSO : ScriptableObject
{
    [field: Header("General Configs")]
    [field: SerializeField] public float Speed { get; protected set; } = 4f;
    [field: SerializeField] public float JumpForce { get; protected set; } = 6f;
    [field: SerializeField] public float SlideForce { get; protected set; } = 12f;
    [field: SerializeField] public int NormalDamage { get; protected set; } = 5;
}
