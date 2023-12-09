using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("References")]
    public Waypoint nextWaypoint;

    public float GetDistance(Vector3 actor){
        return (transform.position - actor).magnitude;
        
    }

    public Vector3 GetDirection(Vector3 actor){
        Vector3 distanceVector = actor - transform.position;
        return distanceVector.normalized;
    }

    public Waypoint GetNextWaypoint(){
        return nextWaypoint;
    }

    public Vector3 GetPosition(){
        return transform.position;
    }

    private void Update(){
        Debug.DrawLine(transform.position, nextWaypoint.transform.position, Color.blue);
    }
}
