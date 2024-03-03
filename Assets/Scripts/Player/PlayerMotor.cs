using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    [Header("Constants")]
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 0.75f;

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
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
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
            Debug.Log(controller.transform.position);
            controller.transform.position = spawnPoint.position;
        }
    }

    // Receive the inputs for our InputManager.cs and apply them to our character controller.
    public void ProcessMoveOld( Vector2 input )
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void ProcessMove( Vector2 input) // bugs: one input stops all movement when jumping, but not two inputs? momentum is carried through based on player rotation. A fixed global position would be better
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = (!controller.isGrounded && input.x == 0) ? 0.8f * Mathf.Sqrt(Mathf.Abs(lastxInput)) * (lastxInput/Mathf.Abs(lastxInput)) : input.x; //if the player is in the air and not inputting any directions, slow them down to keep momentum
        moveDirection.z = (!controller.isGrounded && input.y == 0) ? 0.8f * Mathf.Sqrt(Mathf.Abs(lastzInput)) * (lastzInput/Mathf.Abs(lastzInput)) : input.y;
        lastxInput = moveDirection.x;
        lastzInput = moveDirection.z;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    public void Jump()
    {
        if(isGrounded) 
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
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
        if (sprinting)
            speed = 8;
        else
            speed = 5;
    }
}
