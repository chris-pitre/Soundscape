using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CameraModeComponent cameraMode;

    
    
    
    void Start()
    {
        SetCameraPlayer();
    }

    // Update is called once per frame
    void Update()
    {

        Camera.main.transform.position = cameraMode.SetCamera();
        
    }

    public void SetCameraPlayer()
    {
        cameraMode.setMode(CameraMode.Center, new Vector3());
    }

    public void SetCameraFixed(Vector3 cameraPoint)
    {
        cameraMode.setMode(CameraMode.Fixed, cameraPoint);
    }
}
