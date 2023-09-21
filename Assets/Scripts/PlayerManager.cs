using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MovementComponent movement;
    [SerializeField] private VisionComponent vision;
    
    private void FixedUpdate() {
        Movement();
        Vision();
    }

    private void Movement(){
        float x_input = Input.GetAxisRaw("Horizontal");
        float y_input = Input.GetAxisRaw("Vertical");
        bool isWalking = Input.GetKey(KeyCode.LeftShift);
        Vector2 input = new Vector2(x_input, y_input);

        movement.DoMovement(input, isWalking);
    }

    private void Vision(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        vision.LookAt(mousePos);
    }
}
