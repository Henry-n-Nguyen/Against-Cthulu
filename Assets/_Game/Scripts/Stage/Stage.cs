using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public Transform spawnPoint;
    public PolygonCollider2D mapBound;

    [field: SerializeField] public bool IsEndStage { get; private set; } = false;

    public void OnCompleteStage()
    {
        IsEndStage = true;
    }
}
