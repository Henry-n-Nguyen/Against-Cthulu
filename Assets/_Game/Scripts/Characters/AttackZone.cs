using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    [SerializeField] private List<Damageable> detectedTargetList = new List<Damageable>();
    [SerializeField] private Collider2D col;

    public bool HasTargetInRange
    {
        get
        {
            int i = 0;

            while (i < detectedTargetList.Count)
            {
                if (!detectedTargetList[i].IsAlive) detectedTargetList.Remove(detectedTargetList[i]);
                else i++;
            }

            return detectedTargetList.Count > 0;
        }
        private set
        {
            HasTargetInRange = value;
        }
    }

    public List<Damageable> GetTargetList()
    {
        return detectedTargetList;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable target = collision.GetComponent<Damageable>();
        detectedTargetList.Add(target);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Damageable target = collision.GetComponent<Damageable>();
        detectedTargetList.Remove(target);
    }
}
