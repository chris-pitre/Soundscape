using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionComponent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform actorTransform;
    [Header("Settings")]
    [SerializeField] private bool isRotate;
    private Quaternion direction;
    private bool isLookTowardsRunning = false;

    public void LookAt(Vector3 target){
        Vector3 perpendicular = Vector3.Cross(actorTransform.position-target,Vector3.forward);
		direction = Quaternion.LookRotation(Vector3.forward, perpendicular);
        if(isRotate){
            actorTransform.rotation = direction;
        }

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
            direction = Quaternion.Lerp(originalRotation, finalRotation, timer);
            if(isRotate){
                actorTransform.rotation = direction;
            }
            
            yield return null;
        }
        isLookTowardsRunning = false;
    }

    public void LookFasterTowards(Vector3 target){
        if(!isLookTowardsRunning){
            StartCoroutine(LookFasterTowardsCoroutine(target));
        }
    }

    private IEnumerator LookFasterTowardsCoroutine(Vector3 target){
        isLookTowardsRunning = true;
        Vector3 perpendicular = Vector3.Cross(actorTransform.position-target,Vector3.forward);
        Quaternion originalRotation = actorTransform.rotation;
		Quaternion finalRotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
        float lerpTime = .2f;
        float currentLerpTime = 0f;
        float timer = 0f;
        while(currentLerpTime < lerpTime){
            currentLerpTime += Time.deltaTime;

            timer = currentLerpTime / lerpTime;
            direction = Quaternion.Lerp(originalRotation, finalRotation, timer);
            if(isRotate){
                actorTransform.rotation = direction;
            }
            
            yield return null;
        }
        isLookTowardsRunning = false;
    }


    public void StopLookTowards(){
        StopAllCoroutines();
    }

    public bool GetIsLookTowardsRunning(){
        return isLookTowardsRunning;
    }

    public Quaternion getDirection(){
        return direction;
    }

    public float getDirectionAngle(){
        return direction.eulerAngles.z;
    }
}
