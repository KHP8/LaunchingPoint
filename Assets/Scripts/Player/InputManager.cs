using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.PlayerActions player;
    public PlayerInput.UIActions ui;

    public GameObject playerObject;
    public Transform playerTransform;

    private PlayerMotor motor;
    private PlayerLook look;
    //private PlayerShoot shoot;
    private PlayerMelee melee;
    private PauseMenu pause;
    //private PlayerFireball fireball;

    private BaseAbility primaryAbility;
    private BaseAbility specialQAbility;
    // private PlayerAbility ability;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        playerInput = new PlayerInput();
        player = playerInput.Player;
        ui = playerInput.UI;

        playerTransform = playerObject.transform;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        //shoot = GetComponent<PlayerShoot>();
        melee = GetComponent<PlayerMelee>();
        pause  = GetComponent<PauseMenu>();

        AddComponents();
        
        player.Pause.performed += ctx => pause.PauseManager();
        ui.Pause.performed += ctx => pause.PauseManager();

        player.Jump.performed += ctx => motor.Jump();
        player.Crouch.performed += ctx => motor.Crouch();
        player.Sprint.performed += ctx => motor.Sprint();
        player.Melee.performed += ctx => melee.Melee();

        //player.LeftClick.performed += ctx => shoot.StartFiring();
        //player.LeftClick.canceled += ctx => shoot.StopFiring();
        //player.RightClick.performed += ctx => fireball.StartFiring();
        //player.RightClick.canceled += ctx => fireball.StopFiring();

        player.RightClick.performed += ctx => primaryAbility.UseAbility();
        player.RightClick.canceled += ctx => primaryAbility.StopAbility();
        player.Q.performed += ctx => specialQAbility.UseAbility();
    }

    private void AddComponents()
    {
        if (PlayerPrefs.GetString("Primary") == "Fireball")
        {
            Debug.Log("Fireball added to Primary");
            playerObject.AddComponent<Fireball>();
            playerObject.GetComponent<Fireball>().projectileSource = playerObject.transform.Find("ProjectileSource");
            primaryAbility = GetComponent<Fireball>();
        }
        
        // Special Abilities
        if (PlayerPrefs.GetString("SpecialQ") == "Scorch")
        {
            Debug.Log("Scorch added to SpecialQ");
            playerObject.AddComponent<Scorch>();
            playerObject.GetComponent<Scorch>().beamSource = playerTransform;
            playerObject.GetComponent<Scorch>().parent = playerObject.transform.Find("PlayerBody");
            specialQAbility = GetComponent<Scorch>();
        }

        Debug.Log("Primary: " + PlayerPrefs.GetString("Primary"));
        Debug.Log("SpecialQ: " + PlayerPrefs.GetString("SpecialQ"));
    }

    // To be implemented later
    public void ResetIM()
    {
        playerInput = new PlayerInput();

        player = playerInput.Player;
        ui = playerInput.UI;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        //shoot = GetComponent<PlayerShoot>();
        melee = GetComponent<PlayerMelee>();
        pause  = GetComponent<PauseMenu>();
        //fireball = GetComponent<PlayerFireball>();

        player.Pause.performed += ctx => pause.PauseManager();
        ui.Pause.performed += ctx => pause.PauseManager();

        player.Jump.performed += ctx => motor.Jump();
        player.Crouch.performed += ctx => motor.Crouch();
        player.Sprint.performed += ctx => motor.Sprint();
        player.Melee.performed += ctx => melee.Melee();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (pause.isPaused == false)
            //tell the playermotor to move using the value from our movement action
            motor.ProcessMove(player.Movement.ReadValue<Vector2>());
    }
    private void LateUpdate()
    {
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

    private void Update()
    {
        //Debug.Log("x: " + playerTransform.transform.position.x + "  z: " + playerTransform.transform.position.z);
        playerTransform = playerObject.transform;
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
