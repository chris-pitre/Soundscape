using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MovementComponent movement;
    [SerializeField] private VisionComponent vision;
    [SerializeField] private InventoryComponent inventory;
    
    private void FixedUpdate() {
        Movement();
        Vision();
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            GameObject itemObject = new GameObject("Thrown Item"); // Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.Euler(0f,0f,0f)) as GameObject;
            Item item = itemObject.AddComponent<Item>();
            Rigidbody2D itemrb = itemObject.AddComponent<Rigidbody2D>();
            itemObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(inventory.GetItem(0));
            item.itemData = inventory.GetItem(0).itemType;
            item.Use();
        }
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
