using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Transform spawnPoint;
    [field: SerializeField] public bool IsEndStage { get; private set; } = false;
}
