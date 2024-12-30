using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SciptableObjects/Data/MagicDataSO")]
public class MagicDataSO : ScriptableObject
{
    [field: SerializeField] public List<Magic> magics { get; private set; }

    public Magic GetRandomMagic()
    {
        int randInt = Random.Range(0, magics.Count);

        return magics[randInt];
    }
}
