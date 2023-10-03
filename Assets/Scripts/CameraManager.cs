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
        
    }

    // Update is called once per frame
    void Update()
    {
        SetFixedCamera(new Vector3 (10, 10, -10));
        Camera.main.transform.position = cameraMode.cameraPos;
        
    }

    public void CenterPlayer()
    {
        cameraMode.SetCamera(CameraMode.Center, new Vector3());
    }

    public void SetFixedCamera(Vector3 cameraPoint)
    {
        cameraMode.SetCamera(CameraMode.Fixed, cameraPoint);
    }
}
