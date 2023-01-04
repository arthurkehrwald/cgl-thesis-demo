using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class TrackingReceiver : MonoBehaviour
{
    public Vector3 TrackedPosMeters { get; private set; }

    [SerializeField]
    private int port = 4242;
    [SerializeField]
    private float scalingFactor = 0.1f;

    private Thread receiveThread;
    private UdpClient udpClient;

    private void OnEnable()
    {
        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void OnDisable()
    {
        receiveThread?.Abort();
        receiveThread = null;
        udpClient.Close();
        udpClient = null;
    }

    private void ReceiveData()
    {
        if (udpClient != null)
            udpClient.Close();
        udpClient = new UdpClient(port);
        while(true)
        {
            try
            {
                IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, port);
                byte[] receivedBytes = udpClient.Receive(ref ipEndPoint);                
                double[] receivedCoords = new double[3];

                for (int i = 0; i < 3; i++)
                {                    
                    receivedCoords[i] = BitConverter.ToDouble(receivedBytes, i * 8);
                }

                TrackedPosMeters = new Vector3(
                    (float)receivedCoords[0],
                    (float)receivedCoords[1],
                    (float)receivedCoords[2]) * scalingFactor;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}