using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDebugLines : MonoBehaviour
{
    private void Update()
    {
        Vector3 localBottomLeft = transform.TransformPoint(-0.5f, -0.5f, 0f);
        Vector3 localBottomRight = transform.TransformPoint(0.5f, -0.5f, 0f);
        Vector3 localTopLeft = transform.TransformPoint(-0.5f, 0.5f, 0f);
        Vector3 localTopRight = transform.TransformPoint(0.5f, 0.5f, 0f);

        Debug.DrawLine(localBottomLeft, localBottomRight, Color.black, 0f, false);
        Debug.DrawLine(localBottomRight, localTopRight, Color.black, 0f, false);
        Debug.DrawLine(localTopRight, localTopLeft, Color.black, 0f, false);
        Debug.DrawLine(localTopLeft, localBottomLeft, Color.black, 0f, false);
    }
}
