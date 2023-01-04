using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

[RequireComponent(typeof(Camera))]
public class OffAxisCamera : MonoBehaviour
{
    private new Camera camera;
    [SerializeField]
    private Transform screen;

    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        camera.worldToCameraMatrix = ViewMatrixManual();
        camera.projectionMatrix = ProjectionMatrix(camera.worldToCameraMatrix, 0.5f, 15.0f);
#if UNITY_EDITOR
        CameraUtils.DrawFrustum(camera, Color.red);
#endif
    }

    private Matrix4x4 ViewMatrixManual()
    {
        Matrix4x4 viewMatrix = Matrix4x4.identity;

        viewMatrix[0, 0] = screen.right.x;
        viewMatrix[0, 1] = screen.right.y;
        viewMatrix[0, 2] = screen.right.z;

        viewMatrix[1, 0] = screen.up.x;
        viewMatrix[1, 1] = screen.up.y;
        viewMatrix[1, 2] = screen.up.z;

        // In Unity world space forward is positive z, but in view space it's negative z (I love Unity)
        viewMatrix[2, 0] = -screen.forward.x;
        viewMatrix[2, 1] = -screen.forward.y;
        viewMatrix[2, 2] = -screen.forward.z;

        return viewMatrix * Matrix4x4.Translate(-transform.position);
    }

    private Matrix4x4 ProjectionMatrix(Matrix4x4 viewMatrix, float nearClip, float farClip)
    {
        Vector4 screenBotLeft_OS = new Vector4(-0.5f, -0.5f, 0.0f, 1.0f);
        Vector4 screenBotRight_OS = new Vector4(0.5f, -0.5f, 0.0f, 1.0f);
        Vector4 screenTopLeft_OS = new Vector4(-0.5f, 0.5f, 0.0f, 1.0f);

        Vector4 screenBotLeft_WS = screen.localToWorldMatrix * screenBotLeft_OS;
        Vector4 screenBotRight_WS = screen.localToWorldMatrix * screenBotRight_OS;
        Vector4 screenTopLeft_WS = screen.localToWorldMatrix * screenTopLeft_OS;

        Vector4 screenBotLeft_VS = viewMatrix * screenBotLeft_WS;
        Vector4 screenBotRight_VS = viewMatrix * screenBotRight_WS;
        Vector4 screenTopLeft_VS = viewMatrix * screenTopLeft_WS;

        Vector3 screenRight_VS = (screenBotRight_VS - screenBotLeft_VS).normalized;
        Vector3 screenUp_VS = (screenTopLeft_VS - screenBotLeft_VS).normalized;
        Vector3 screenForward_VS = Vector3.Cross(screenRight_VS, screenUp_VS).normalized;

        float screenSpaceOriginDist = -Vector3.Dot(screenForward_VS, screenBotLeft_VS);

        float l = Vector3.Dot(screenRight_VS, screenBotLeft_VS) * nearClip / screenSpaceOriginDist;
        float r = Vector3.Dot(screenRight_VS, screenBotRight_VS) * nearClip / screenSpaceOriginDist;
        float t = Vector3.Dot(screenUp_VS, screenTopLeft_VS) * nearClip / screenSpaceOriginDist;
        float b = Vector3.Dot(screenUp_VS, screenBotLeft_VS) * nearClip / screenSpaceOriginDist;

        return Matrix4x4.Frustum(l, r, b, t, nearClip, farClip);
    }
}
