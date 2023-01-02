using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedPosDriver : MonoBehaviour
{
    [SerializeField]
    private TrackingReceiver trackingReceiver;
    
    void Update()
    {
        transform.localPosition = trackingReceiver.TrackedPosMeters;
    }
}
