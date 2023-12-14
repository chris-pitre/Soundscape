using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockedList : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> blocked; 
    [SerializeField] private int width;
    [SerializeField] private int height;
    public void Awake(){
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("blocked");
        blocked = new List<Vector2>();
        
        foreach (GameObject block in blocks){
            Vector3 thisBlock = block.transform.position;
            
            Vector3 originPosition = new Vector3(-(width/2f), - (height/2f), 0f);
            
            thisBlock.x = Mathf.FloorToInt((thisBlock).x);
            thisBlock.y = Mathf.FloorToInt((thisBlock).y);
            
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y));
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y+1));
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y));
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y+1));
            Debug.Log("this got added ");
        
        }
     
        
    }

    public List<Vector2> getBlockedList(){
        if (blocked.Count == 0){
         GameObject[] blocks = GameObject.FindGameObjectsWithTag("blocked");
        blocked = new List<Vector2>();
        
        foreach (GameObject block in blocks){
            Vector3 thisBlock = block.transform.position;
            
            Vector3 originPosition = new Vector3(-(width/2f), - (height/2f), 0f);
            
            thisBlock.x = Mathf.FloorToInt((thisBlock).x);
            thisBlock.y = Mathf.FloorToInt((thisBlock).y);
            
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y));
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y+1));
            blocked.Add(new Vector2(thisBlock.x+1 , thisBlock.y));
            blocked.Add(new Vector2(thisBlock.x , thisBlock.y+1));
            Debug.Log("this got added ");
            
            }
            return blocked;
            }else{
                return blocked;
            }
             
    
}
}
