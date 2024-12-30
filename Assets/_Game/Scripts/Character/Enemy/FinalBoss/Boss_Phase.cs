using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Phase : MonoBehaviour
{
    [field: SerializeField] public bool IsCompletePhase { get; protected set; }

    public virtual void OnCompletePhase() { 
        IsCompletePhase = true;
    }

}
