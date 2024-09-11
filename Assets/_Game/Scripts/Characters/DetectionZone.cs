using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    [SerializeField] private List<Collider2D> detectedCol = new List<Collider2D>();
    [SerializeField] private Collider2D col;

    public bool HasTargetInRange 
    {
        get
        {
            return detectedCol.Count > 0;
        }
        private set
        {
            HasTargetInRange = value;
        } 
    } 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedCol.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedCol.Remove(collision);
    }
}
