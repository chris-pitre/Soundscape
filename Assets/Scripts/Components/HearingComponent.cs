using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearingComponent : MonoBehaviour
{
    private Collider2D entered = null;
    private Collider2D exited = null;
    private void OnTriggerEnter2D(Collider2D other) {
        entered = other;
    }

    private void OnTriggerExit2D(Collider2D other) {
        exited = other;
    }

    public Collider2D GetEntered(){
        Collider2D temp = entered;
        entered = null;
        return temp;
    }

    public Collider2D GetExited(){
        Collider2D temp = exited;
        exited = null;
        return temp;
    }

}