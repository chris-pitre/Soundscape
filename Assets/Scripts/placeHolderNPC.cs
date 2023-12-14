using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeHolderNPC : MonoBehaviour
{
    public EnemyVision vision;
    public Rigidbody2D rb;
    public float timer = 0; 
    public GameObject player;
    public Vector3 origin;
    private float originRotate;
    public bool reset = false; 
    public bool resetAble = false; 
    public float resetTimer;
    void Start()
    {
        origin = transform.position;
        originRotate = rb.rotation;
    }

    
    void Update()
    {
        if(!reset){
            if(!vision.playerInVision){
                if(!resetAble){
                if(timer <= 3f){
                    rb.velocity = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad),Mathf.Sin(rb.rotation * Mathf.Deg2Rad)).normalized;
                    timer += Time.deltaTime;
                }else{
                    timer = 0f;
                    rb.rotation += 180;
                }}
                else{
                    if(resetTimer <= 3f){
                        resetTimer += Time.deltaTime;
                    }else{
                        resetAble = false;
                        resetTimer = 0f;
                        reset = true;
                    }
                    }
            }else{
                Quaternion rotation = Quaternion.LookRotation(player.transform.position - transform.position, transform.TransformDirection(Vector3.up));
                transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                rb.velocity = new Vector2(Mathf.Cos(rb.rotation * Mathf.Deg2Rad),Mathf.Sin(rb.rotation * Mathf.Deg2Rad)).normalized * 0.5f;
                resetAble = true;
                resetTimer = 0f;
            }
        }else{
            if((origin - transform.position).magnitude > 1f){
                transform.position = origin;
                rb.rotation = originRotate;
                reset = false;
            }
           
        }      
    }
}
