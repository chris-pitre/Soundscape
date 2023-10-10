using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public GameObject player;
    public float FOV;
     public float visionRange;
     public bool playerInVision;
     public GameObject enemy;
     public LayerMask theMask;
 
    void Start()
    {
        
    }


    void FixedUpdate()
    {       float upperAngle = transform.eulerAngles.z + FOV/2; 
            float lowerAngle = transform.eulerAngles.z - FOV/2; 

            Vector3 upperDir = new Vector3(Mathf.Cos(upperAngle * Mathf.Deg2Rad),Mathf.Sin(upperAngle * Mathf.Deg2Rad) , 0);
            Vector3 lowerDir = new Vector3(Mathf.Cos(lowerAngle * Mathf.Deg2Rad),Mathf.Sin(lowerAngle * Mathf.Deg2Rad) , 0);
            
            Debug.DrawRay(enemy.transform.position, upperDir * visionRange, Color.red);
            Debug.DrawRay(enemy.transform.position, lowerDir * visionRange, Color.red);
            

            Vector3 theAngle =  player.transform.position - enemy.transform.position;
            float theDeg = Vector3.Angle(theAngle, transform.right);
            
            
        if(theAngle.magnitude <= visionRange){
            if(theDeg <= FOV/2){
                Debug.DrawRay(enemy.transform.position, theAngle.normalized * visionRange, Color.green);
                RaycastHit2D hit = Physics2D.Raycast(enemy.transform.position, theAngle.normalized, visionRange, theMask);
                
                if(hit.collider != null && hit.transform == player.transform){
                playerInVision = true;
                Debug.DrawLine(enemy.transform.position, hit.point, Color.blue);
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
