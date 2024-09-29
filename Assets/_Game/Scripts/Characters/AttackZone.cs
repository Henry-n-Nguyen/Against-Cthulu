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
        CollideWithCharacter(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EndCollideWithCharacter(collision);
    }

    private void CollideWithCharacter(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(S_Constant.TAG_PLAYER) && !collision.gameObject.CompareTag(S_Constant.TAG_ENEMY))
            return;

        Damageable target = collision.GetComponent<Damageable>();
        detectedTargetList.Add(target);
    }

    private void EndCollideWithCharacter(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(S_Constant.TAG_PLAYER) && !collision.gameObject.CompareTag(S_Constant.TAG_ENEMY))
            return;

        Damageable target = collision.GetComponent<Damageable>();
        detectedTargetList.Remove(target);
    }
}
