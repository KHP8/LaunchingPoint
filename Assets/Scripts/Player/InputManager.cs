using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerShoot shoot;
    private PlayerMelee melee;
    private PlayerFireball fireball;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        shoot = GetComponent<PlayerShoot>();
        melee = GetComponent<PlayerMelee>();
        fireball = GetComponent<PlayerFireball>();

        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Sprint.performed += ctx => motor.Sprint();
        onFoot.Shoot.performed += ctx => shoot.StartFiring();
        onFoot.Shoot.canceled += ctx => shoot.StopFiring();
        onFoot.Melee.performed += ctx => melee.Melee();
        onFoot.Fireball.performed += ctx => fireball.StartFiring();
        onFoot.Fireball.canceled += ctx => fireball.StopFiring();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from our movement action
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate() {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
    private void OnEnable() 
    {
        onFoot.Enable();
    }
    private void OnDisable()
    {
        onFoot.Disable();
    }
}
