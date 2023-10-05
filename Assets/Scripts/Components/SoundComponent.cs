using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    private CircleCollider2D cc;
    private float soundTimerCountdown = 0f;

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
        soundTimerCountdown = 0;
        StartCoroutine(DoSoundImpulse(noiseLevel, soundTimer));
    }

    private IEnumerator DoSoundImpulse(float noiseLevel, float soundTimer){
        while(soundTimerCountdown < soundTimer){
            cc.radius = Mathf.Lerp(cc.radius, noiseLevel, Time.deltaTime * 10);
            soundTimerCountdown += Time.deltaTime;
            yield return null;
        }
    }


}
