using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfVision))]
public class visionEditor : Editor
{
  void OnSceneGUI() {
    FieldOfVision fov = (FieldOfVision)target;
    Handles.color = Color.white;
    Handles.DrawWireArc(fov.transform.position, Vector3.forward, Vector3.right, 360, fov.viewSize);
    Vector3 viewAngleA = fov.DirFromAngle(-fov.FOV/2, false);
    Vector3 viewAngleB = fov.DirFromAngle(fov.FOV/2, false);
    
    Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleA * fov.viewSize);
    Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngleB * fov.viewSize);
  }
}
