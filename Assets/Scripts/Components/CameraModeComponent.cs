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
    public CameraMode mode;

    public Vector3 SetCamera()
    {
        switch (mode)
        {
            case CameraMode.Center:
                mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector3 playerPos = playerTransform.position;
                return new Vector3(playerPos.x + mousePos.x - 0.5f, playerPos.y + mousePos.y - 0.5f, -10);
                
            case CameraMode.Fixed:
                return cameraPos;

            default:
                return new Vector3(0, 0, -10);

        }
        
    }

    public void setMode(CameraMode newMode, Vector3 pos)
    {
        mode = newMode;
        cameraPos = pos;
    }
    
}
