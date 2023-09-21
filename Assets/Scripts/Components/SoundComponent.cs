using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    private CircleCollider2D cc;
    private void Start(){
        cc = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        cc.radius = 0;
        cc.isTrigger = true;
        cc.enabled = true;
    }
    public void MakeSound(float noiseLevel){
        cc.radius = Mathf.Lerp(cc.radius, noiseLevel, Time.deltaTime * 10);
    }
}
