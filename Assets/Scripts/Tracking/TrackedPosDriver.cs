using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedPosDriver : MonoBehaviour
{
    [SerializeField]
    private TrackingReceiver trackingReceiver;

    private Vector3 startingPos;

    private void Awake()
    {
        startingPos = transform.localPosition;
    }

    private void Start()
    {
        trackingReceiver.trackingStopped.AddListener(OnTrackingReceiverStopped);
    }

    private void OnTrackingReceiverStopped()
    {
        transform.localPosition = startingPos;
    }

    void Update()
    {
        if (trackingReceiver.IsTrackedPosUpToDate)
            transform.localPosition = trackingReceiver.TrackedPosMeters;
    }
}
