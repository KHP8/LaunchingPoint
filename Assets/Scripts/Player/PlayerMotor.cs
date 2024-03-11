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
    public float playerHeight = 2f;
    public LayerMask whatIsGround;
    public bool isGrounded;

    [Header("Constants")]
    public float speed = 15f;
    public float gravity = -9.8f;
    public float jumpHeight = 0.75f;
    public float groundDrag = 0.5f;
    public float airDrag = 1;
    public float jumpCooldown = 0.2f;
    public float airMultiplier = 0.25f;
    public bool readyToJump;

    public float speedDisplay;

    private bool crouching = false;
    private float crouchTimer = 1;
    private bool lerpCrouch = false;
    private bool sprinting = false;
    private float lastxInput;
    private float lastzInput;

    [Header("Respawn")]
    public float minHeight = -30f;
    public Transform spawnPoint;

    

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

        //increase gravity
        playerRigidbody.AddForce(Vector3.down * (-1*gravity-9.8f), ForceMode.Force);

        // Apply ground drag
        if (isGrounded)
            playerRigidbody.drag = groundDrag;
        else
            playerRigidbody.drag = airDrag;

        //limit speed
        SpeedControl();

        /*isGrounded = controller.isGrounded;
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer * crouchTimer;
            if (crouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }


        // check if needs to be respawned
        if (controller.transform.position[1] < minHeight) {
            UnityEngine.Debug.Log(controller.transform.position);
            controller.transform.position = spawnPoint.position;
        }*/
    }


    public void ProcessMove(Vector2 input)
    {
        moveDirection = orientation.forward * input.y + orientation.right * input.x;
        
        //grounded
        if(isGrounded)
            playerRigidbody.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
        //in air
        else if(!isGrounded)
            playerRigidbody.AddForce(moveDirection.normalized * speed * airMultiplier * 10f, ForceMode.Force);

    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(playerRigidbody.velocity.x, 0f, playerRigidbody.velocity.z);
        speedDisplay = flatVel.magnitude;

        //limit velocity
        if(flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            playerRigidbody.velocity = new Vector3(limitedVel.x, playerRigidbody.velocity.y, limitedVel.z);
        }
    }

    public void Jump()
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

    public void Crouch()
    {
        crouching = !crouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (!sprinting)
            speed = speed * (2f/3f);
        else
            speed += speed/2f;
    }
}
