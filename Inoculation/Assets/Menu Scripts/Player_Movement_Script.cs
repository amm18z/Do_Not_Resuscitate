using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Regular move speed
    public float sprintSpeedMultiplier = 2.0f; // Sprint speed multiplier
    public float collisionOffset = 0.05f;
    private Vector2 movementInput;
    public ContactFilter2D movementFilter;
    private Rigidbody2D rb;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private float originalMoveSpeed;
    public saveGame game;

    public Animator animator;
    public Boolean isMovingVerticalFirst;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalMoveSpeed = moveSpeed;
        game.LoadGame(); // last save on start
    }

    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            float currentSpeed = moveSpeed;

            //Animation stuff goes here
            animator.SetFloat("VerticalMovement", movementInput.y);
            animator.SetFloat("HorizontalMovement", movementInput.x);

            // Check if the player is holding down the sprint key (Left Shift)
            if (Keyboard.current.leftShiftKey.isPressed)
            {
                currentSpeed *= sprintSpeedMultiplier;
            }

            int count = rb.Cast(
                movementInput,
                movementFilter,
                castCollisions,
                currentSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0)
            {
                rb.MovePosition(rb.position + movementInput * currentSpeed * Time.fixedDeltaTime);
            }
        }
        else
        {
            animator.SetFloat("VerticalMovement", 0);
            animator.SetFloat("HorizontalMovement", 0);
        }

        //Originally added to prevent flipping if player is moving vertically but leads to more issues
        /*if(movementInput.x == 0)
        {
            if (movementInput.y != 0)
            {
                isMovingVerticalFirst = true;
            }
            else
            {
                isMovingVerticalFirst = false;
            }
        }*/

        if(movementInput.x < 0 && transform.localScale.x > 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        if (movementInput.x > 0 && transform.localScale.x < 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnSprint(InputValue sprintValue)
    {
        // You can toggle the sprint by setting a boolean flag or handle sprint differently
        // Here's a simple example:
        if (sprintValue.isPressed)
        {
            moveSpeed = originalMoveSpeed * sprintSpeedMultiplier;
        }
        else
        {
            moveSpeed = originalMoveSpeed;
        }
    }
}