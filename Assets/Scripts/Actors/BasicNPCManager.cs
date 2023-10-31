using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicNPCManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PatrolComponent patrol;
    //eventually figure out how to combine these two components
    [SerializeField] private EnemyVision enemyVision;
    [SerializeField] private VisionComponent vision;
    [SerializeField] private MovementComponent movement;
    //eventually when a* gets implemented, that will handle chasing the player
    //for now, kinematic arrive
    [SerializeField] private Transform playerPosition;
    private Vector2 lastPosition;
    private Vector2 direction;
    //end of temp stuff

    [Header("Options")]
    [SerializeField] private EnemyStates state;
    [SerializeField] private float cautionTime = 10f;
    [SerializeField] private float radiusOfSat = 0.2f;

    private float timer = 0f;
    private void FixedUpdate() {
        switch(state){
            case EnemyStates.Alert:
                direction = playerPosition.position - transform.position;
                movement.DoMovement(direction.normalized, false);
                vision.LookAt(playerPosition.position);
                if(!enemyVision.playerInVision){
                    timer = 0f;
                    lastPosition = playerPosition.position;
                    state = EnemyStates.Cautious;
                }
                break;
            case EnemyStates.Patrol:
                patrol.DoPatrol();
                if(enemyVision.playerInVision){
                    patrol.StopPatrol();
                    state = EnemyStates.Alert;
                }
                break;
            case EnemyStates.Cautious:
                direction = enemyVision.hit.point - new Vector2(transform.position.x, transform.position.y);
                if(direction.magnitude > radiusOfSat){
                    vision.LookAt(lastPosition - direction);
                    movement.DoMovement(direction.normalized, false);
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

}