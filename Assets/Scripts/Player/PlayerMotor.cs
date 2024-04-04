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

    [Header("Grounded Check")]
    public float playerHeight = 2f; // How tall the player is. Important for the ground check raycast
    public LayerMask whatIsGround; // The layermask for the raycast to use for detecting the ground
    public bool isGrounded; // True when player is on a surface recognized as ground

    [Header("Ground Control")]
    public float speed = 15f; // Also acts as the max speed for the player
    public float groundDrag = 0.5f; // Drag applied to the player when on the ground
    public float jumpCooldown = 0.2f; // Cooldown so jumps cant be doubled up on
    public float sprintSpeedPercent = 0.5f; // What percent to increase speed by when sprinting
    public float acceleration = 10f; // How much to accelerate the player by. Does not affect max speed

    [Header("Air Control")]
    public float airSpeed = 15f; // Acts as the max speed when the player is in the air
    public float airDrag = 1; // Drag applied to the player when in the air
    public float airMultiplier = 0.25f; // Affects how strong the player's control is in the air (lower means less control)
    public float jumpHeight = 0.75f; // How powerful or high the player's jumps are
    public float gravity = -9.8f; // Gravity
    public float airAccel = 10f; // How much to accelerate the player by when in the air. Does not affect max speed
    
    [Header("Stats Display")]
    public float speedDisplay; // Displays the player's speed for debugging purposes
    public bool readyToJump; // Shows whether or not the player can jump
    public bool sprinting = false; // Shows whether player is sprinting
    
    private bool crouching = false;
    private float crouchTimer = 1;
    private bool lerpCrouch = false;
    
    private float lastxInput;
    private float lastzInput;

    

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRigidbody.freezeRotation = true;
        ResetJump();
    }

    // Update is called once per frame
    void Update()
    {
        // Grounded Check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        // Modulate gravity
        playerRigidbody.AddForce(Vector3.down * (-1*gravity-9.8f), ForceMode.Force);

        // Apply the appropriate drag value
        if (isGrounded)
            playerRigidbody.drag = groundDrag;
        else
            playerRigidbody.drag = airDrag;

        // Limit velocity to speed variable
        SpeedControl();
    }


    public void ProcessMove(Vector2 input)
    {
        moveDirection = orientation.forward * input.y + orientation.right * input.x;
        
        // Grounded
        if(isGrounded)
            playerRigidbody.AddForce(moveDirection.normalized * speed * acceleration, ForceMode.Force);

        // In air use airMultiplier
        else if(!isGrounded)
            playerRigidbody.AddForce(moveDirection.normalized * speed * airMultiplier * airAccel, ForceMode.Force);

    }

    private void SpeedControl() // Limits the player's speed to the speed variable
    {
        Vector3 flatVel = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
        speedDisplay = flatVel.magnitude;

        // Limit velocity
        if((flatVel.magnitude > speed) && isGrounded || (flatVel.magnitude > airSpeed) && !isGrounded)
        {
            Vector3 limitedVel = flatVel.normalized * (isGrounded ? speed : airSpeed);
            playerRigidbody.velocity = new Vector3(limitedVel.x, playerRigidbody.velocity.y, limitedVel.z);
        }
    }

    public void Jump() // Applies a force upwards to jump. Affected by jumpHeight and jumpCooldown
    {
        if(isGrounded && readyToJump) 
        {   
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
            playerRigidbody.AddForce(orientation.up * 10f * jumpHeight, ForceMode.Impulse);
            readyToJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    public void Crouch() // Not functional atm
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (!sprinting)
            speed = speed * (1f/(1f+sprintSpeedPercent)); // Return to original speed
        else
            speed = speed * (1f+sprintSpeedPercent); // Increase speed by sprintSpeedPercent
    }
}
