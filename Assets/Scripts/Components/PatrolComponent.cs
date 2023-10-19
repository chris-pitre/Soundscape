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
    private void Start(){
        transform.position = currentWaypoint.transform.position;
        movement = GetComponent<MovementComponent>();
        vision = GetComponent<VisionComponent>();
    }

    private void Update(){
        if(vision.GetIsLookTowardsRunning()){
            movement.DoMovementSilent(Vector2.zero, false);
            return;
        }
        if(currentWaypoint.GetDistance(transform.position) <= waypointThreshold){
            movement.DoMovementSilent(Vector2.zero, false);
            currentWaypoint = currentWaypoint.GetNextWaypoint();
            vision.LookTowards(currentWaypoint.transform.position);
        } else {
            movement.DoMovement(currentWaypoint.GetDirection(transform.position), false);
            vision.LookAt(currentWaypoint.transform.position);
        }
    }


}
