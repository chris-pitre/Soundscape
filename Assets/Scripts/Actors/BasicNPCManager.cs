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
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform rotationTransform;
    [SerializeField] private SoundComponent sound;
    [SerializeField] private AudioComponent audioComp;
    private Vector2 lastPosition;
    private Vector2 itemPosition;
    private Vector2 direction;
    private bool heard = false;
    private bool seen = false;

    [Header("Options")]
    [SerializeField] private EnemyStates state;
    [SerializeField] private float cautionTime = 10f;
    [SerializeField] private float radiusOfSat = 0.2f;

    private float timer = 0f;
    private void Start(){
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
		agent.updateUpAxis = false;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0f);
    }

    private float walkTimer = 0f;
    private readonly float maxWalkTimer = 0.5f;
  
    private void FixedUpdate() {
        DoHearing();
        HandleFade();
        
        if(agent.velocity != Vector3.zero){
            sound.MakeSoundConstant(4f);
            walkTimer += Time.deltaTime;
            if(walkTimer >= maxWalkTimer){
                walkTimer = 0f;
                audioComp.PlayFootstep();
            }
            anim.SetBool("Walking", true);
        } else {
            sound.MakeSoundConstant(0f);
            walkTimer = 0f;
            anim.SetBool("Walking", false);
        }

        float angle = rotationTransform.eulerAngles.z;
        Vector2 angleVector = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        anim.SetFloat("moveX", angleVector.x);
        anim.SetFloat("moveY", angleVector.y);

        switch(state){
            case EnemyStates.Alert:
                patrol.StopPatrol();
                agent.speed = 4f;
                agent.SetDestination(playerPosition.position);
                vision.LookFasterTowards(transform.position + (agent.velocity.normalized));
                if(!enemyVision.playerInVision && !heard){
                    timer = 0f;
                    lastPosition = playerPosition.position;
                    state = EnemyStates.Cautious;
                }
                break;
            case EnemyStates.Patrol:
                agent.speed = 1.5f;
                if(enemyVision.playerInVision){
                    state = EnemyStates.Alert;
                }
                patrol.DoPatrol();
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
            case EnemyStates.Investigating:
                agent.SetDestination(itemPosition);
                vision.LookFasterTowards(transform.position + (agent.velocity.normalized));
                timer += Time.deltaTime;
                if(timer >= cautionTime){
                    state = EnemyStates.Patrol;
                }
                if(enemyVision.playerInVision){
                    state = EnemyStates.Alert;
                }
                break;
        } 
    }

    private void DoHearing(){
        Collider2D entered = sound.GetEntered();
        if(entered != null)
            GetHearingEnter(entered);
        Collider2D exited = sound.GetExited();
        if(exited != null)
            GetHearingExit(exited);
    }


   private void GetHearingEnter(Collider2D other){
        switch(other.tag){
           case "Player":
                heard = true;
                break;
            case "ThrownItem":
                if(state == EnemyStates.Alert){
                    return;
                }
                itemPosition = other.transform.position;
                timer = 0f;
                state = EnemyStates.Investigating;
                break;
        }
    }

    private void GetHearingExit(Collider2D other){
        switch(other.tag){
            case "Player":
                heard = false;
                break;
        }
    }

    public void SetAlert(Vector3 alertPos){
        lastPosition = alertPos;
        vision.LookFasterTowards(transform.position + (agent.velocity.normalized));
        state = EnemyStates.Alert;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        switch(other.tag){
            case "Vision":
                seen = true;
                break;
        } 
    }

    private void OnTriggerExit2D(Collider2D other) {
        switch(other.tag){
            case "Vision":
                seen = false;
                break;
        }
    }

    private void HandleFade(){
        if(heard || seen){
            FadeIn();
        } 
        if(!heard && !seen){
            FadeOut();
        }
        
        float newAlpha = Mathf.MoveTowards(sprite.color.a, fadeLevel, Time.deltaTime * 2f);
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, newAlpha);
    }

    private float fadeLevel = 0f;
    public void FadeIn(){
        fadeLevel = 1f;
    }

    public void FadeOut(){
        fadeLevel = 0f;
    }
}