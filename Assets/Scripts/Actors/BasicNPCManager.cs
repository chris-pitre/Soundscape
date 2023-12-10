using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicNPCManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PatrolComponent patrol;
    [SerializeField] private EnemyVision enemyVision;
    [SerializeField] private VisionComponent vision;
    [SerializeField] private MovementComponent movement;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform playerPosition;
    private Vector2 lastPosition;
    private Vector2 direction;

    [Header("Options")]
    [SerializeField] private EnemyStates state;
    [SerializeField] private float cautionTime = 10f;
    [SerializeField] private float radiusOfSat = 0.2f;

    private float timer = 0f;
    private void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
    }
  
    private void FixedUpdate() {
        switch(state){
            case EnemyStates.Alert:
                patrol.StopPatrol();
                agent.speed = 2.5f;
                agent.SetDestination(playerPosition.position);
                vision.LookFasterTowards(transform.position + (agent.velocity.normalized));
                if(!enemyVision.playerInVision){
                    timer = 0f;
                    lastPosition = playerPosition.position;
                    state = EnemyStates.Cautious;
                }
                break;
            case EnemyStates.Patrol:
                agent.speed = 1.5f;
                patrol.DoPatrol();
                if(enemyVision.playerInVision){
                    state = EnemyStates.Alert;
                }
                break;
            case EnemyStates.Cautious:
                direction = enemyVision.hit.point - new Vector2(transform.position.x, transform.position.y);
                if(direction.magnitude > radiusOfSat){
                    agent.SetDestination(lastPosition);
                    vision.LookFasterTowards(transform.position + (agent.velocity.normalized));
                }
                timer += Time.deltaTime;
                if(enemyVision.playerInVision){
                    state = EnemyStates.Alert;
                }
                if(timer >= cautionTime){
                    state = EnemyStates.Patrol;
                    patrol.StartPatrol();
                }
                break;
        } 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
        if(other.tag == "Player"){
            lastPosition = other.transform.position;
            vision.LookFasterTowards(transform.position + (agent.velocity.normalized));
            state = EnemyStates.Alert;
        }
    }
 
}