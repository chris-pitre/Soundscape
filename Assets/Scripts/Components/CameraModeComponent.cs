using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode
{
    Center,
    Fixed
}
public class CameraModeComponent : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    public Vector3 cameraPos;
    public Vector3 mousePos;

    public void SetCamera(CameraMode mode, Vector3 cameraPoint)
    {
        switch (mode)
        {
            case CameraMode.Center:
                mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector3 playerPos = playerTransform.position;
                cameraPos = new Vector3(playerPos.x + mousePos.x - 0.5f, playerPos.y + mousePos.y - 0.5f, -10);
                break;
            case CameraMode.Fixed:
                cameraPos = cameraPoint;
                break;
        }
    }

    
}
