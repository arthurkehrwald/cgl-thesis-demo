using System.Collections.Generic;
using UnityEngine;

public class GateTriggerZone : MonoBehaviour
{
    public bool IsBodyInZone { get; private set; } = false;
    private readonly List<Collider> collidersInTrigger = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;
        if (!collidersInTrigger.Contains(other))
            collidersInTrigger.Add(other);
        IsBodyInZone = collidersInTrigger.Count != 0;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;
        if (collidersInTrigger.Contains(other))
            collidersInTrigger.Remove(other);
        IsBodyInZone = collidersInTrigger.Count != 0;
    }
}
