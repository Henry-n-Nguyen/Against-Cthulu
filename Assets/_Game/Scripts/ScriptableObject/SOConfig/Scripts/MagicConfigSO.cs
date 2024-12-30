using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HuySpace;

[CreateAssetMenu(menuName = "SciptableObjects/Config/MagicConfigSO")]
public class MagicConfigSO : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string MagicName { get; private set; }
    [field: TextArea][field: SerializeField] public string Description { get; private set; }
    [field: SerializeField] public float CD { get; private set; }
    [field: SerializeField] public float Multiplier { get; private set; }
    [field: SerializeField] public MagicDeployType DeployType { get; private set; }
}
