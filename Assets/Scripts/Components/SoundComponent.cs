using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    private CircleCollider2D cc;
    [SerializeField] private HearingComponent hearing;
    private GameObject soundObject;

    private void Awake(){
        soundObject = new GameObject("Sound Object");
        soundObject.transform.SetParent(transform, false);
        //soundObject.transform.position = transform.position;
        soundObject.layer = 10;
        cc = soundObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        cc.radius = 0;
        cc.isTrigger = true;
        cc.enabled = true;

        hearing = soundObject.AddComponent(typeof(HearingComponent)) as HearingComponent;
        Rigidbody2D rb = soundObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void LateUpdate() {
        soundObject.transform.localPosition = Vector3.zero;    
    }

    public void MakeSoundConstant(float noiseLevel){
        cc.radius = Mathf.Lerp(cc.radius, noiseLevel, Time.deltaTime * 10);
        Debug.DrawCircle(transform.position, cc.radius, 16, Color.red);
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
            Debug.DrawCircle(transform.position, cc.radius, 16, Color.red);
            yield return null;
        } 
    }

    public Collider2D GetEntered(){
        return hearing.GetEntered();
    }
    
    public Collider2D GetExited(){
        return hearing.GetExited();
    }


}
