using System.Collections;
using System.Collections.Generic;
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

    [Header("Respawn")]
    public float minHeight = -30f;
    public Transform spawnPoint;

    [Header("Shooting")]
    public Transform projectileSource;
    public GameObject bullet;
    public float rpm;
    public float bulletSpeed;
    
    private bool canShoot = true;
    private WaitForSeconds shootDelay;
    private Coroutine shootCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        shootDelay = new WaitForSeconds(60 / rpm);
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
    public void ProcessMove( Vector2 input )
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

    public void StartFiring()
    {
        shootCoroutine = StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (canShoot)
            {
                canShoot = false;
                Camera cam = GetComponent<PlayerLook>().cam;
                GameObject proj = Instantiate(
                    bullet, 
                    projectileSource.position, 
                    Quaternion.Euler(
                        cam.transform.eulerAngles.x + 90,
                        transform.eulerAngles.y,
                        0
                    )
                );
                proj.GetComponent<Rigidbody>().velocity = cam.transform.forward * bulletSpeed;
                proj.GetComponent<PlayerProjectile>().startPoint = projectileSource.position;
                StartCoroutine(ResetShootCooldown());
            }
            yield return null;
        }
    }

    public void StopFiring()
    {
        if (shootCoroutine != null)
            StopCoroutine(shootCoroutine);
    }

    IEnumerator ResetShootCooldown()
    {
        yield return shootDelay;
        canShoot = true;
    }
}
