using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.PlayerActions player;
    public PlayerInput.UIActions ui;

    private PlayerMotor motor;
    private PlayerLook look;
    private PlayerShoot shoot;
    private PlayerMelee melee;
    private PauseMenu pause;
    private PlayerFireball fireball;
    // private PlayerAbility ability;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerInput = new PlayerInput();
        player = playerInput.Player;
        ui = playerInput.UI;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        shoot = GetComponent<PlayerShoot>();
        melee = GetComponent<PlayerMelee>();
        pause  = GetComponent<PauseMenu>();
        fireball = GetComponent<PlayerFireball>();
        // ability = GetComponent<PlayerAbility>;
        
        player.Pause.performed += ctx => pause.PauseManager();
        ui.Pause.performed += ctx => pause.PauseManager();

        player.Jump.performed += ctx => motor.Jump();
        player.Crouch.performed += ctx => motor.Crouch();
        player.Sprint.performed += ctx => motor.Sprint();
        player.LeftClick.performed += ctx => shoot.StartFiring();
        player.LeftClick.canceled += ctx => shoot.StopFiring();
        player.RightClick.performed += ctx => fireball.StartFiring();
        player.RightClick.canceled += ctx => fireball.StopFiring();
        player.Melee.performed += ctx => melee.Melee();

        // FOR FUTURE USE
        // player.LeftClick.performed += ctx => ability.LeftClickAbility();
        // player.RightClick.performed += ctx => ability.RightClickAbility();
        // player.Q.performed += ctx => ability.QAbility();
        // player.E.performed += ctx => ability.EAbility();
        // player.X.performed += ctx => ability.UltimateAbility();
        // player.C.performed += ctx => ability.DashAbility();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pause.isPaused == false)
            //tell the playermotor to move using the value from our movement action
            motor.ProcessMove(player.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate() {
        if (pause.isPaused == true)
        {
            OnFootDisable();
            UIEnable();
        }
        else
        {
            OnFootEnable();
            UIDisable();
        }

        if (pause.isPaused == false)
            look.ProcessLook(player.Look.ReadValue<Vector2>());
    }

    private void OnFootEnable() 
    {
        player.Enable();
    }

    private void OnFootDisable()
    {
        player.Disable();
    }

    private void UIEnable()
    {
        ui.Enable();
    }

    private void UIDisable()
    {
        ui.Disable();
    }
}
