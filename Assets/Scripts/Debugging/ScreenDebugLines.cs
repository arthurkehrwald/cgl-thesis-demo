using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenDebugLines : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Vector3 localBottomLeft = transform.TransformPoint(-0.5f, -0.5f, 0f);
        Vector3 localBottomRight = transform.TransformPoint(0.5f, -0.5f, 0f);
        Vector3 localTopLeft = transform.TransformPoint(-0.5f, 0.5f, 0f);
        Vector3 localTopRight = transform.TransformPoint(0.5f, 0.5f, 0f);

        Gizmos.DrawLine(localBottomLeft, localBottomRight);
        Gizmos.DrawLine(localBottomRight, localTopRight);
        Gizmos.DrawLine(localTopRight, localTopLeft);
        Gizmos.DrawLine(localTopLeft, localBottomLeft);
    }
}
