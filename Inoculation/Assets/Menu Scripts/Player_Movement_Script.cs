using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float moveSpeed; // Character Move Speed
    public Rigidbody2D playerRigidBody; // Rigid Body (Character)
    private Vector2 playerMoveDirection; // Player Direction (Can be used for animation as well)

    // Used Fixed Update to avoid framerate movement dependency
    private void FixedUpdate()
    {
        // Physics Calculations
        playerMove();
       
    }

    void ProcessInputs()
    {
        // Process Keyboard Inputs
        float movePlayerX = Input.GetAxisRaw("Horizontal"); // 0 or 1 for keyboard input
        float movePlayerY = Input.GetAxisRaw("Vertical");   // Get Vertical Movement
        // Use Get Axis if wanting to add controller support - stretch goal
        playerMoveDirection = new Vector2(movePlayerX, movePlayerY).normalized;

    }

    void playerMove()
    {
        // Move the player
        playerRigidBody.velocity = new Vector2(playerMoveDirection.x * moveSpeed, playerMoveDirection.y * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
       ProcessInputs(); 
    }
}
