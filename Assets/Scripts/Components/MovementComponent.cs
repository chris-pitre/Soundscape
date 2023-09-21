using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to add Movement to an actor with a rigidbody
/// </summary>
[RequireComponent(typeof(SoundComponent))] 

public class MovementComponent : MonoBehaviour
{
    // Parameters
    [Header("Components")]
    [SerializeField] private SoundComponent sound;

    [Header("References")]
    [SerializeField] private Rigidbody2D actorRb;

    [Header("Movement Settings")]
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float walkSpeed = 5f;

    [Header("Sound Settings")]
    [SerializeField] private float runSoundCooldown = 0.5f;
    [SerializeField] private float walkSoundCooldown = 1.0f;
    [SerializeField] private float runSoundNoiseLevel = 2f;
    [SerializeField] private float walkSoundNoiseLevel = 0.5f;

    private float soundTimer = 0.0f;

    private Vector2 smoothedInput;
    private Vector2 input;
    private Vector2 smoothInputCurrentVelocity;
    

    private void Start(){
        sound = GetComponent<SoundComponent>();
    }

    private void Update(){
        if(soundTimer >= 0f){
            soundTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Applies movement to a rigidbody with given input parameters
    /// </summary>
    public void DoMovement(Vector2 input, bool isWalking){
        float speed = isWalking ? walkSpeed : runSpeed; 
                
        smoothedInput = Vector2.SmoothDamp(
            smoothedInput,
            input,
            ref smoothInputCurrentVelocity,
            0.1f,
            speed
        );

        actorRb.velocity = smoothedInput * speed;

        if(input != Vector2.zero && soundTimer <= 0f){
            soundTimer = isWalking ? walkSoundCooldown : runSoundCooldown;
            float noiseLevel = isWalking ? walkSoundNoiseLevel : runSoundNoiseLevel;
            sound.MakeSound(noiseLevel);
        }
    }
}