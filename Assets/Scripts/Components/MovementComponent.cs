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
    [SerializeField] private float runSoundNoiseLevel = 2f;
    [SerializeField] private float walkSoundNoiseLevel = 0.5f;

    [SerializeField] private float smoothValue;
    private Vector2 smoothedInput;
    private Vector2 input;
    private Vector2 smoothInputCurrentVelocity;
    

    private void Start(){
        sound = GetComponent<SoundComponent>();
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
            smoothValue,
            speed
        );

        actorRb.velocity = smoothedInput * speed;

        if(input != Vector2.zero){
            float noiseLevel = isWalking ? walkSoundNoiseLevel : runSoundNoiseLevel;
            sound.MakeSoundConstant(noiseLevel);
        } else {
            sound.MakeSoundConstant(0);
        }
    }

    public void DoMovementSilent(Vector2  input, bool isWalking){
        float speed = isWalking ? walkSpeed : runSpeed; 
                
        smoothedInput = Vector2.SmoothDamp(
            smoothedInput,
            input,
            ref smoothInputCurrentVelocity,
            smoothValue,
            speed
        );

        actorRb.velocity = smoothedInput * speed;
        sound.MakeSoundConstant(0);
    }
}