using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionComponent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform actorTransform;
    private bool isLookTowardsRunning = false;

    public void LookAt(Vector3 target){
        Vector3 perpendicular = Vector3.Cross(actorTransform.position-target,Vector3.forward);
		actorTransform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);

        Debug.DrawRay(actorTransform.position, target - actorTransform.position, Color.green);
    }

    public void LookTowards(Vector3 target){
        if(!isLookTowardsRunning){
            StartCoroutine(LookTowardsCoroutine(target));
        }
    }

    private IEnumerator LookTowardsCoroutine(Vector3 target){
        isLookTowardsRunning = true;
        Vector3 perpendicular = Vector3.Cross(actorTransform.position-target,Vector3.forward);
        Quaternion originalRotation = actorTransform.rotation;
		Quaternion finalRotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
        float lerpTime = 1f;
        float currentLerpTime = 0f;
        float timer = 0f;
        while(currentLerpTime < lerpTime){
            currentLerpTime += Time.deltaTime;

            timer = currentLerpTime / lerpTime;
            actorTransform.rotation = Quaternion.Lerp(originalRotation, finalRotation, timer);
            
            yield return null;
        }
        isLookTowardsRunning = false;
    }

    public bool GetIsLookTowardsRunning(){
        return isLookTowardsRunning;
    }
}
