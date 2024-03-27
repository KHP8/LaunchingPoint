using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private Rigidbody playerRigidbody;
    public Transform orientation;
    private Vector3 moveDirection;
    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        crouch,
        air
    }

    [Header("Grounded Check")]
    public float playerHeight = 2f; // How tall the player is. Important for the ground check raycast
    public LayerMask whatIsGround; // The layermask for the raycast to use for detecting the ground
    public bool isGrounded; // True when player is on a surface recognized as ground

    [Header("Slope Handler")]
    public float maxSlopeAngle; // Max angle of a slope the player can walk on
    private RaycastHit slopeHit; // Raycast for detecting when on a slope

    [Header("Ground Control")]
    public float walkspeed; // The speed the player goes when grounded
    public float sprintSpeed; // The speed the player goes when sprinting
    public float groundDrag = 0.5f; // Drag applied to the player when on the ground
    public float jumpCooldown = 0.2f; // Cooldown so jumps cant be doubled up on
    public float sprintSpeedPercent = 0.5f; // What percent to increase speed by when sprinting
    public float acceleration = 10f; // How much to accelerate the player by. Does not affect max speed
    private float speed; // Also acts as the max speed for the player

    [Header("Air Control")]
    public float airSpeed = 15f; // Acts as the max speed when the player is in the air
    public float airDrag = 1; // Drag applied to the player when in the air
    public float airMultiplier = 0.25f; // Affects how strong the player's control is in the air (lower means less control)
    public float jumpHeight = 0.75f; // How powerful or high the player's jumps are
    public float gravity = -9.8f; // Gravity
    public float airAccel = 10f; // How much to accelerate the player by when in the air. Does not affect max speed

    [Header("Crouching")]
    public float crouchSpeed; // The speed the player goes when crouching
    public float crouchYScale; // How much the player's Y scale should change when crouching
    private float startYScale; // Original Y scale of the player

    [Header("Stats Display")]
    public float speedDisplay; // Displays the player's speed for debugging purposes
    public bool readyToJump; // Shows whether or not the player can jump
    public bool sprinting = false; // Shows whether player is sprinting
    public bool crouching = false; // Shows whether player is crouching
    public bool exitingSlope = false; // Shows whether the player is attempting to exit a slope by jumping

    

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        ResetJump();
        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Grounded Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Modulate gravity
        if(playerRigidbody.useGravity)
            playerRigidbody.AddForce(Vector3.down * (-1*gravity-9.8f), ForceMode.Force);

        // Apply the appropriate drag value
        if (isGrounded)
            playerRigidbody.drag = groundDrag;
        else
            playerRigidbody.drag = airDrag;

        // Limit velocity to speed variable
        SpeedControl();

        // Change the state and speed to the appropriate value
        StateHandler();
    }


    public void ProcessMove(Vector2 input)
    {
        // Find the movement direction from input
        moveDirection = orientation.forward * input.y + orientation.right * input.x;

        // On a slope
        if (OnSlope() && !exitingSlope)
        {
            playerRigidbody.AddForce(GetSlopeMoveDirection() * speed * acceleration * 2f, ForceMode.Force);

            if(playerRigidbody.velocity.y > 0)
            {
                playerRigidbody.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        
        // Grounded
        else if(isGrounded)
            playerRigidbody.AddForce(moveDirection.normalized * speed * acceleration, ForceMode.Force);

        // In air use airMultiplier
        else if(!isGrounded)
            playerRigidbody.AddForce(moveDirection.normalized * speed * airMultiplier * airAccel, ForceMode.Force);

        // Turn off gravity when on a slope
        playerRigidbody.useGravity = !OnSlope();
    }

    private void StateHandler()
    {
        // Crouching
        if (crouching)
        {
            state = MovementState.crouch;
            speed = crouchSpeed;
        }

        // Sprinting
        else if(isGrounded && sprinting)
        {
            state = MovementState.sprinting;
            speed = sprintSpeed;
        }

        // Walking
        else if (isGrounded)
        {
            state = MovementState.walking;
            speed = walkspeed;
        }

        // Air
        else
        {
            state = MovementState.air;
        }
    }

    private void SpeedControl() // Limits the player's speed to the speed variable
    {
        // Limiting the velocity while on a slope
        if (OnSlope() && !exitingSlope)
        {
            if (playerRigidbody.velocity.magnitude > speed)
            {
                playerRigidbody.velocity = playerRigidbody.velocity.normalized * speed;
            }
        }

        else
        {
            Vector3 flatVel = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
            speedDisplay = flatVel.magnitude;

            // Limit velocity
            if ((flatVel.magnitude > speed) && isGrounded || (flatVel.magnitude > airSpeed) && !isGrounded)
            {
                Vector3 limitedVel = flatVel.normalized * (isGrounded ? speed : airSpeed);
                playerRigidbody.velocity = new Vector3(limitedVel.x, playerRigidbody.velocity.y, limitedVel.z);
            }
        }
    }

    public void Jump() // Applies a force upwards to jump. Affected by jumpHeight and jumpCooldown
    {
        if(isGrounded && readyToJump) 
        {
            exitingSlope = true;
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
            playerRigidbody.AddForce(orientation.up * 10f * jumpHeight, ForceMode.Impulse);
            readyToJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    private Vector3 GetSlopeMoveDirection()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }

    private void ResetJump()
    {
        readyToJump = true;
        exitingSlope = false;
    }

    public void Crouch() // Not functional atm
    {
        crouching = !crouching;

        if (crouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            playerRigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    public void Sprint()
    {
        sprinting = !sprinting;
/*        if (!sprinting)
            speed = speed * (1f/(1f+sprintSpeedPercent)); // Return to original speed
        else
            speed = speed * (1f+sprintSpeedPercent); // Increase speed by sprintSpeedPercent*/
    }
}
