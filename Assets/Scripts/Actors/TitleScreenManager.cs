using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private Vector3 startingPos;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Vector3 cameraSpeed;
    void Start()
    {
        cameraManager.SetCameraFixed(startingPos);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        cameraManager.SetCameraFixed(cameraTransform.position + cameraSpeed);
    }
}
