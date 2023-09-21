using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    public void MakeSound(float noiseLevel){
        StartCoroutine(LerpCircleRadius(noiseLevel));
    }

    public IEnumerator LerpCircleRadius(float noiseLevel){
        CircleCollider2D cc = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        cc.radius = 0;
        cc.enabled = true;
        while(cc.radius < noiseLevel - 0.05f){
            cc.radius = Mathf.Lerp(cc.radius, noiseLevel, Time.deltaTime);
            yield return null;
        }
        cc.enabled = false;
        Object.Destroy(cc);
    }
}
