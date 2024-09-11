using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] private List<Damageable> detectedTarget = new List<Damageable>();
    [SerializeField] private Collider2D col;

    public bool HasTargetInRange
    {
        get
        {
            return detectedTarget.Count > 0;
        }
        private set
        {
            HasTargetInRange = value;
        }
    }

    public List<Damageable> GetTargetList()
    {
        return detectedTarget;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable target = collision.GetComponent<Damageable>();
        detectedTarget.Add(target);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Damageable target = collision.GetComponent<Damageable>();
        detectedTarget.Remove(target);
    }
}
