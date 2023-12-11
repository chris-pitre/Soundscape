using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VisionComponent))]
public class PatrolComponent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private VisionComponent vision;
    [SerializeField] private Waypoint currentWaypoint;
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;

    [Header("Settings")]
    [SerializeField] private float waypointThreshold;
    [SerializeField] private float waitTime = 0f;

    private bool countdown = false;
    public float timer;
    public bool isWaiting = false;
    public bool isStarting = false;

    private void Start() {
        transform.position = currentWaypoint.transform.position;
        vision = GetComponent<VisionComponent>();
        timer = waitTime;
    }

    public void StartPatrol() {
        timer = waitTime;
        countdown = false;
        DoPatrol();
    }

    public void DoPatrol()
    {
        if (currentWaypoint.GetDistance(transform.position) <= waypointThreshold) {
            if (timer <= 0f) {
                countdown = false;
                timer = waitTime;
                currentWaypoint = currentWaypoint.GetNextWaypoint();
                agent.SetDestination(currentWaypoint.GetPosition());
                vision.LookTowards(currentWaypoint.GetPosition());
            } else {
                if (!countdown) {
                    countdown = true;
                }
                vision.LookTowards(currentWaypoint.GetNextWaypoint().GetPosition());
            }
        } else {
            agent.SetDestination(currentWaypoint.GetPosition());
            vision.LookFasterTowards(transform.position + (agent.velocity.normalized));
        }
    }

    void Update() {
        if (countdown) {
            timer = timer - Time.deltaTime;
        }
    }

    public void StopPatrol() {
        countdown = false;
        timer = waitTime;
        StopAllCoroutines();
    }
}
