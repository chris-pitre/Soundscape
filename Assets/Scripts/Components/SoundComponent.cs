using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    private CircleCollider2D cc;
    [SerializeField] private HearingComponent hearing;
    [SerializeField] private Material lineMat;
    private GameObject soundObject;
    private LineRenderer circleRenderer;

    private void Awake(){
        soundObject = new GameObject("Sound Object");
        soundObject.transform.SetParent(transform, false);
        soundObject.layer = 10;
        cc = soundObject.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
        cc.radius = 0f;
        cc.isTrigger = true;
        cc.enabled = true;

        hearing = soundObject.AddComponent(typeof(HearingComponent)) as HearingComponent;
        Rigidbody2D rb = soundObject.AddComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Static;

        circleRenderer = soundObject.AddComponent(typeof(LineRenderer)) as LineRenderer;
        circleRenderer.useWorldSpace = false;
        circleRenderer.widthMultiplier = 0.05f;
        circleRenderer.numCapVertices = 6;
        circleRenderer.numCornerVertices = 6;
        circleRenderer.material = lineMat;
        circleRenderer.startColor = Color.red;
        circleRenderer.endColor = Color.red;
    }

    private void LateUpdate() {
        soundObject.transform.localPosition = Vector3.zero;
        DrawCircle(cc.radius);  
    }

    public void MakeSoundConstant(float noiseLevel){
        cc.radius = Mathf.Lerp(cc.radius, noiseLevel, Time.deltaTime * 10);
        Debug.DrawCircle(transform.position, cc.radius, 6, Color.red);
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

    private void DrawCircle(float radius){
        int segments = (int) radius * 12;
        circleRenderer.positionCount = segments;

        for(int i = 0; i < segments; i++){
            float circumferenceProgress = (float) i / (segments-1);
            float currentRadian = circumferenceProgress * 2f * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian + Random.Range(-0.2f, 0.2f));
            float yScaled = Mathf.Sin(currentRadian + Random.Range(-0.2f, 0.2f));

            float x = radius * xScaled;
            float y = radius * yScaled;
            float z = 0f;

            Vector3 currentPosition = new Vector3(x, y, z);

            circleRenderer.SetPosition(i, currentPosition);
        }
    }


}
