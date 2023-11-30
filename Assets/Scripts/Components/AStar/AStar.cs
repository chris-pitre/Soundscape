using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    private Pathfinding pathfinding;
    private List<Node> path;
    private List<Vector2> blocked; 
    
    private Vector3 previousTargetPos;
    [SerializeField] private MovementComponent movement;
    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] private float satisfactionRange;
    private Vector2 distance;
    
    void Awake()
    {
        blocked = GameObject.Find("AiGrid").GetComponent<blockedList>().getBlockedList();
        Debug.Log("blockeed balue = " +blocked.Count);
        pathfinding = new Pathfinding(width, height,blocked);
    }
    public void doMove(Vector3 target, bool silent){
        if(target == previousTargetPos){
            
            distance = new Vector3(getNodeCords().x , getNodeCords().y, 0f) - this.gameObject.transform.position;
            if(distance.magnitude > satisfactionRange){
                
                movement.DoMovement(distance.normalized, silent);
            }else{

                if(path.Count > 1){
                   
                    path.RemoveAt(0);
                }
            }
            
        }else{
            pathFind(target);
             
            previousTargetPos = target;
            distance = new Vector3(getNodeCords().x , getNodeCords().y, 0f) - this.gameObject.transform.position;
            if(distance.magnitude > satisfactionRange){
                movement.DoMovement(distance.normalized, silent);
            }
        }
    }
    public void doMoveSilent(Vector3 target, bool silent){
        if(target == previousTargetPos){
            
            distance = new Vector3(getNodeCords().x , getNodeCords().y, 0f) - this.gameObject.transform.position;
            if(distance.magnitude > satisfactionRange){
                
                movement.DoMovementSilent(distance.normalized, silent);
            }else{

                if(path.Count > 1){
                  
                    path.RemoveAt(0);
                }
            }
            
        }else{
            pathFind(target);
            
            previousTargetPos = target;
            distance = new Vector3(getNodeCords().x , getNodeCords().y, 0f) - this.gameObject.transform.position;
            if(distance.magnitude > satisfactionRange){
                movement.DoMovementSilent(distance.normalized, silent);
            }
        }
    }
 

  private void pathFind(Vector3 target){
        
        path = pathfinding.FindPath((int)this.gameObject.transform.position.x, (int)this.gameObject.transform.position.y, (int)target.x, (int)target.y);
       
    }
    public Vector3 getNodeCords(){
        
        return pathfinding.GetWorldNode(path[0].x, path[0].y);
    }
}
