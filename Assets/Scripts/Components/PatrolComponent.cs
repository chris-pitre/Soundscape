using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementComponent))] 
[RequireComponent(typeof(VisionComponent))] 

public class PatrolComponent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MovementComponent movement;
    [SerializeField] private VisionComponent vision;
    [SerializeField] private Waypoint currentWaypoint;

    [Header("Settings")]
    [SerializeField] private float waypointThreshold;
    [SerializeField] private float waitTime = 0f;
    private float timer;
    private bool isWaiting = false;
    private bool isStarting = false;

    private void Start(){
        transform.position = currentWaypoint.transform.position;
        movement = GetComponent<MovementComponent>();
        vision = GetComponent<VisionComponent>();
        timer = waitTime;
    }
    
    public void StartPatrol(){
        StopAllCoroutines();
        vision.StopLookTowards();
        StartCoroutine(StartPatrolCoroutine());
    }

    public IEnumerator StartPatrolCoroutine(){
        isStarting = true;
        vision.LookTowards(currentWaypoint.transform.position); 
        while(vision.GetIsLookTowardsRunning()){
            yield return null;
        }
        while(currentWaypoint.GetDistance(transform.position) > waypointThreshold){
            movement.DoMovement(currentWaypoint.GetDirection(transform.position), false);
            vision.LookAt(currentWaypoint.transform.position);
            yield return null;
        }
        isStarting = false;
    }

    public void DoPatrol(){
        if(isStarting){
            return;
        }
        if(vision.GetIsLookTowardsRunning()){
            movement.DoMovementSilent(Vector2.zero, false);
            return;
        } 
        if(timer < waitTime){
            timer += Time.deltaTime;
            movement.DoMovementSilent(Vector2.zero, false);
            return;
        } else if (isWaiting && timer >= waitTime){
            currentWaypoint = currentWaypoint.GetNextWaypoint();
            vision.LookTowards(currentWaypoint.transform.position);
            isWaiting = false;
        } 
        if(currentWaypoint.GetDistance(transform.position) <= waypointThreshold){
            movement.DoMovementSilent(Vector2.zero, false);
            isWaiting = true;
            timer = 0f;
        } else {
            movement.DoMovement(currentWaypoint.GetDirection(transform.position), false);
            vision.LookAt(currentWaypoint.transform.position);
        }
    }

    public void StopPatrol(){
        StopAllCoroutines();
        vision.StopLookTowards();
    }


}
