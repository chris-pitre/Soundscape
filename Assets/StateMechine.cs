using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

 public enum AIState
{
    Patrol,
    Cautious,
    Alert
}


public class StateMechine : MonoBehaviour
{
    public AIState currentState;
    [SerializeField] private EnemyVision vision;
    [SerializeField] private float cautionDuration;
    IEnumerator timer;
    void Start()
    {
        currentState = AIState.Patrol;
    }

    void Update()
    {
        if (vision.playerInVision && currentState != AIState.Alert)
        {
            currentState = AIState.Alert;
            StopAllCoroutines();
        }
        if (!vision.playerInVision && currentState == AIState.Alert)
        {
            currentState = AIState.Cautious;
            timer = startTimer(cautionDuration);
            StartCoroutine(timer);
        }
        
    }

    public void setState(AIState state)
    {
        currentState = state;
    }

    public IEnumerator startTimer(float time)
    {
        yield return new WaitForSeconds(time);
        currentState = AIState.Patrol;
    }
}
