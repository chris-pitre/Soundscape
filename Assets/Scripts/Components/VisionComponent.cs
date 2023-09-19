using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionComponent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform actorTransform;

    public void LookAt(Vector3 target){
        Vector3 perpendicular = Vector3.Cross(actorTransform.position-target,Vector3.forward);
		actorTransform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

        Debug.DrawRay(actorTransform.position, target - actorTransform.position, Color.green);
    }
}
