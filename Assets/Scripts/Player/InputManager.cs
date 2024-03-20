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
    private PlayerMelee melee;
    private PauseMenu pause;

    private BaseAbility primaryAbility;
    private BaseAbility secondaryAbility;
    private BaseAbility specialQAbility;
    private BaseAbility specialEAbility;

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
        melee = GetComponent<PlayerMelee>();
        pause  = GetComponent<PauseMenu>();

        AddComponents();
        
        player.Pause.performed += ctx => pause.PauseManager();
        ui.Pause.performed += ctx => pause.PauseManager();

        player.Jump.performed += ctx => motor.Jump();
        player.Crouch.performed += ctx => motor.Crouch();
        player.Sprint.performed += ctx => motor.Sprint();
        player.Melee.performed += ctx => melee.Melee();

        player.RightClick.performed += ctx => primaryAbility.UseAbility();
        player.RightClick.canceled += ctx => primaryAbility.StopAbility();
        player.LeftClick.performed += ctx => secondaryAbility.UseAbility();
        player.LeftClick.canceled += ctx => secondaryAbility.StopAbility();
        player.Q.performed += ctx => specialQAbility.UseAbility();
        player.E.performed += ctx => specialEAbility.UseAbility();
    }

    private void AddComponents()
    {
        if (PlayerPrefs.GetString("Primary") == "Fireball")
        {
            Debug.Log("Fireball added to Primary");
            playerObject.AddComponent<Fireball>();
            playerObject.GetComponent<Fireball>().projectileSource = playerObject.transform.Find("PlayerBody").Find("ProjectileSource");
            primaryAbility = GetComponent<Fireball>();
        }

        if (PlayerPrefs.GetString("Secondary") == "RockThrow")
        {
            Debug.Log("Rock Throw added to Secondary");
            playerObject.AddComponent<RockThrow>();
            playerObject.GetComponent<RockThrow>().projectileSource = playerObject.transform.Find("PlayerBody").Find("ProjectileSource");
            secondaryAbility = GetComponent<RockThrow>();
        }

        // SpecialQ Abilities
        if (PlayerPrefs.GetString("SpecialQ") == "Scorch")
        {
            Debug.Log("Scorch added to SpecialQ");
            playerObject.AddComponent<Scorch>();
            playerObject.GetComponent<Scorch>().parent = playerObject.transform.Find("PlayerBody");
            specialQAbility = GetComponent<Scorch>();
        }
        else if (PlayerPrefs.GetString("SpecialQ") == "FireWave")
        {

        }

        // SpecialE Abilities
        if (PlayerPrefs.GetString("SpecialE") == "Scorch")
        {
            Debug.Log("Scorch added to SpecialQ");
            playerObject.AddComponent<Scorch>();
            playerObject.GetComponent<Scorch>().parent = playerObject.transform.Find("PlayerBody");
            specialEAbility = GetComponent<Scorch>();
        }
        else if (PlayerPrefs.GetString("SpecialE") == "FireWave")
        {

        }

        Debug.Log("Primary: " + PlayerPrefs.GetString("Primary"));
        Debug.Log("SpecialQ: " + PlayerPrefs.GetString("SpecialQ"));
        Debug.Log("SpecialE: " + PlayerPrefs.GetString("SpecialE"));
    }

    // To be implemented later
    //public void ResetIM()
    //{
    //    playerInput = new PlayerInput();

    //    player = playerInput.Player;
    //    ui = playerInput.UI;

    //    motor = GetComponent<PlayerMotor>();
    //    look = GetComponent<PlayerLook>();
    //    melee = GetComponent<PlayerMelee>();
    //    pause  = GetComponent<PauseMenu>();

    //    player.Pause.performed += ctx => pause.PauseManager();
    //    ui.Pause.performed += ctx => pause.PauseManager();

    //    player.Jump.performed += ctx => motor.Jump();
    //    player.Crouch.performed += ctx => motor.Crouch();
    //    player.Sprint.performed += ctx => motor.Sprint();
    //    player.Melee.performed += ctx => melee.Melee();
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        //if (pause.isPaused == false)
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
        playerTransform = playerObject.transform;

        if (playerObject.GetComponent<Scorch>() != null)
        {
            Vector3 tempScorchPosition = new Vector3(
                playerObject.transform.Find("PlayerBody").transform.position.x,
                playerObject.transform.Find("PlayerBody").transform.position.y + 1f,
                playerObject.transform.Find("PlayerBody").transform.position.z);

            playerObject.GetComponent<Scorch>().beamSource = tempScorchPosition;
        }

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
