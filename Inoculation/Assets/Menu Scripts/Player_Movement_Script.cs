using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



// Takes and handles input and movement for a player character
public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 2f; // speed serialized
    public float collisionOffset = 0.05f; // how far the player can be + the collision box
    private Vector2 movementInput; // movement input
    public ContactFilter2D movementFilter; // movement filter
    private Rigidbody2D rb; // player rigid body
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();   

    // Start is called before the first frame update
    void Start()
    {
        // get the rigid body
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate() {
        if (movementInput != Vector2.zero)
        {
            // get position, and movement filter 
            int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                moveSpeed*Time.fixedDeltaTime + collisionOffset); 
            
            if (count == 0)
            {
                // change rigid bodys position
                rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);

            }
        }
    }
   

    void OnMove(InputValue movementValue)
    {
        // Set the movement input
        movementInput = movementValue.Get<Vector2>();
       
    }

   

    

   
}
