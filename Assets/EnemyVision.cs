using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform rotationTransform;
    public GameObject player;
    public float FOV;
    public float visionRange;
    public bool playerInVision;
    public LayerMask theMask;
    public RaycastHit2D hit; 
 

    void FixedUpdate()
    {       float upperAngle = transform.eulerAngles.z + FOV/2; 
            float lowerAngle = transform.eulerAngles.z - FOV/2; 

            Vector3 upperDir = new Vector3(Mathf.Cos(upperAngle * Mathf.Deg2Rad),Mathf.Sin(upperAngle * Mathf.Deg2Rad) , 0);
            Vector3 lowerDir = new Vector3(Mathf.Cos(lowerAngle * Mathf.Deg2Rad),Mathf.Sin(lowerAngle * Mathf.Deg2Rad) , 0);
            
            Debug.DrawRay(transform.position, upperDir * visionRange, Color.red);
            Debug.DrawRay(transform.position, lowerDir * visionRange, Color.red);
            

            Vector3 theAngle =  player.transform.position - transform.position;
            float theDeg = Vector3.Angle(theAngle, transform.right);
            
            
        if(theAngle.magnitude <= visionRange){
            if(theDeg <= FOV/2){
                Debug.DrawRay(transform.position, theAngle.normalized * visionRange, Color.green);
                hit = Physics2D.Raycast(transform.position, theAngle.normalized, visionRange, theMask);
                
                if(hit.collider != null && hit.transform == player.transform){
                playerInVision = true;
                Debug.DrawLine(transform.position, hit.point, Color.blue);
                }else{
                    playerInVision = false;
                }
            }else{
                    playerInVision = false;
                }
        }else{
            playerInVision = false;
        }
        
       
    }
}
