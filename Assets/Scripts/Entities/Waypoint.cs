using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Waypoint nextWaypoint;

    public float GetDistance(Vector3 actor){
        return Vector3.Distance(transform.position, actor);
    }

    public Vector3 GetDirection(Vector3 actor){
        Vector3 distanceVector = transform.position - actor;
        return distanceVector / distanceVector.magnitude;
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
