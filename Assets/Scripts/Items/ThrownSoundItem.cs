using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SoundComponent))]
[RequireComponent(typeof(AudioSource))]
public class ThrownSoundItem : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SoundComponent sound;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource audioSource;
    private Vector2 initialVelocity;
    private bool collectable = false;

    private void LateUpdate() {
        if(!collectable && rb.velocity.magnitude <= 0.1f){
            collectable = true;
        }
    }
    public void Throw(Vector2 target, float speed){
        rb.velocity = target * speed;
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 10f);
        initialVelocity = rb.velocity;
    }

    public bool isCollectable(){
        return collectable;
    }

    private void OnCollisionEnter2D(Collision2D other){
        Debug.Log(other);
        sound.MakeSoundImpulse(3f, 2f);
        audioSource.Play();
    }
}
