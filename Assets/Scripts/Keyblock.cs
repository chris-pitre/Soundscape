using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyblock : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log(other);
        switch (other.tag){
            case "Player":
                if(other.gameObject.GetComponent<PlayerManager>().GetKeys() >= 3){
                    Destroy(this.gameObject);
                }
                break;
        }
   } 

}
