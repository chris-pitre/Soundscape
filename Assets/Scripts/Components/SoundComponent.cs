using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    private CircleCollider2D cc;

    private void Awake(){
        cc = gameObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        cc.radius = 0;
        cc.isTrigger = true;
        cc.enabled = true;
    }
    public void MakeSoundConstant(float noiseLevel){
        cc.radius = Mathf.Lerp(cc.radius, noiseLevel, Time.deltaTime * 10);
    }

    public void MakeSoundImpulse(float noiseLevel, float soundTimer){
        StartCoroutine(DoSoundImpulse(noiseLevel, soundTimer));
    }

    private IEnumerator DoSoundImpulse(float noiseLevel, float soundTimer){
        float lerpTime = soundTimer;
        float timer = 0f;
        float soundTimerCountdown = 0f;
        cc.radius = noiseLevel;
        while(soundTimerCountdown < lerpTime){
            soundTimerCountdown += Time.deltaTime;
            timer = soundTimerCountdown / lerpTime;
            timer = Mathf.Sin(timer * Mathf.PI * 0.5f);

            cc.radius = Mathf.Lerp(cc.radius, 0, timer);
            Debug.DrawRay(transform.position, Vector2.up * cc.radius, Color.red);
            yield return null;
        } 
    }


}
