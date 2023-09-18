using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Component to add Movement to an actor with a rigidbody
/// </summary>
public class MovementComponent : MonoBehaviour
{
    // Parameters
    [Header("References")]
    [SerializeField] private Rigidbody2D actorRb;

    [Header("Movement Settings")]
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float walkSpeed = 5f;

    private Vector2 smoothedInput;
    private Vector2 input;
    private Vector2 smoothInputCurrentVelocity;
    
    /// <summary>
    /// Applies movement to a rigidbody with given input parameters
    /// </summary>
    public void DoMovement(float x_input, float y_input, bool isWalking){
        float speed = isWalking ? walkSpeed : runSpeed; 
        
        input = new Vector2(x_input, y_input);
        smoothedInput = Vector2.SmoothDamp(
            smoothedInput,
            input,
            ref smoothInputCurrentVelocity,
            0.1f,
            speed
        );

        actorRb.velocity = smoothedInput * speed;
    }
}