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
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    //[SerializeField] private AStar astar;

    [Header("Settings")]
    [SerializeField] private float waypointThreshold;
    [SerializeField] private float waitTime = 0f;
    private bool countdown = false;
    public float timer;
    public bool isWaiting = false;
    public bool isStarting = false;

    private void Start(){
        transform.position = currentWaypoint.transform.position;
        movement = GetComponent<MovementComponent>();
        vision = GetComponent<VisionComponent>();
        //astar = GetComponent<AStar>();
        timer = waitTime;
    }
    
    public void StartPatrol(){
        DoPatrol();
        timer = waitTime;
    }

    

    public void DoPatrol(){
        
        if(currentWaypoint.GetDistance(transform.position) <= waypointThreshold){
            
            if(timer <= 0f){
            countdown = false;
            timer = waitTime;
            currentWaypoint = currentWaypoint.GetNextWaypoint();
            agent.SetDestination(currentWaypoint.GetPosition());
            vision.LookTowards(currentWaypoint.GetPosition());
            }else{
                 if (!countdown){countdown = true;}
                    vision.LookTowards(currentWaypoint.GetNextWaypoint().GetPosition()); 
                    }
        }else{
            
            agent.SetDestination(currentWaypoint.GetPosition());
            vision.LookTowards(currentWaypoint.GetPosition());
        }}
        void Update(){
            if(countdown){
            timer = timer - Time.deltaTime;
        }}
        
        
       /* if(isStarting){
            return;
        }
        if(vision.GetIsLookTowardsRunning()){
           
            movement.DoMovementSilent(Vector2.zero, false);
            return;
        } 
        if(timer < waitTime){
            timer += Time.deltaTime;
            
           //astar.doMoveSilent(currentWaypoint.GetPosition(), false);
           agent.SetDestination(currentWaypoint.GetPosition());
            return;
        } else if (isWaiting && timer >= waitTime){
            currentWaypoint = currentWaypoint.GetNextWaypoint();
            vision.LookTowards(currentWaypoint.transform.position);
            isWaiting = false;
        } 
        if(currentWaypoint.GetDistance(transform.position) <= waypointThreshold){
            
             //astar.doMoveSilent(currentWaypoint.GetPosition(), false);
             agent.SetDestination(currentWaypoint.GetPosition());
            isWaiting = true;
            timer = 0f;
        } else {
            
            agent.SetDestination(currentWaypoint.transform.position);
             //astar.doMove(currentWaypoint.transform.position, false);
            vision.LookAt(currentWaypoint.transform.position);
        }
    }
*/
    public void StopPatrol(){
        StopAllCoroutines();
        vision.StopLookTowards();
    }


}
