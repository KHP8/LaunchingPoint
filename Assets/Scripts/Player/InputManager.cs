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
    private BaseAbility ultimateAbility;

    // UI Icons
    public CooldownIcon qIcon;
    public CooldownIcon eIcon;
    public CooldownIcon ultimateIcon;

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
        pause = GetComponent<PauseMenu>();

        AddComponents();

        player.Pause.performed += ctx => pause.PauseManager();
        ui.Pause.performed += ctx => pause.PauseManager();

        player.Jump.performed += ctx => motor.Jump();
        player.Crouch.performed += ctx => motor.Crouch();
        player.Sprint.performed += ctx => motor.Sprint();
        player.Melee.performed += ctx => melee.Melee();

        player.LeftClick.performed += ctx =>
        {
            primaryAbility.UseAbility();
            primaryAbility.StopAbility();
        };
        player.Q.performed += ctx =>
        {
            specialQAbility.UseAbility();
            specialQAbility.StopAbility();
            qIcon.CooldownSelector(specialQAbility.cooldownFloat);
        };
        player.E.performed += ctx =>
        {
            specialEAbility.UseAbility();
            specialEAbility.StopAbility();
            eIcon.CooldownSelector(specialEAbility.cooldownFloat);
        };
        player.X.performed += ctx =>
        {
            ultimateAbility.UseAbility();
            ultimateIcon.CooldownSelector(ultimateAbility.cooldownFloat);
        };
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
            Debug.Log("FireWave added to SpecialQ");
            playerObject.AddComponent<FireWave>();
            playerObject.GetComponent<FireWave>().projectileSource = playerObject.transform.Find("PlayerBody").Find("ProjectileSource");
            specialQAbility = GetComponent<FireWave>();
        }

        // SpecialE Abilities
        if (PlayerPrefs.GetString("SpecialE") == "Scorch")
        {
            Debug.Log("Scorch added to SpecialE");
            playerObject.AddComponent<Scorch>();
            playerObject.GetComponent<Scorch>().parent = playerObject.transform.Find("PlayerBody");
            specialEAbility = GetComponent<Scorch>();
        }
        else if (PlayerPrefs.GetString("SpecialE") == "FireWave")
        {
            Debug.Log("FireWave added to SpecialE");
            playerObject.AddComponent<FireWave>();
            playerObject.GetComponent<FireWave>().projectileSource = playerObject.transform.Find("PlayerBody").Find("ProjectileSource");
            specialEAbility = GetComponent<FireWave>();
        }


        // Ultimate Abilities
        if (PlayerPrefs.GetString("Ultimate") == "FireNuke")
        {
            Debug.Log("FireNuke added to Ultimate");
            playerObject.AddComponent<FireNuke>();
            playerObject.GetComponent<FireNuke>().parent = playerObject.transform.Find("PlayerBody");
            ultimateAbility = GetComponent<FireNuke>();
        }

        Debug.Log("Primary: " + PlayerPrefs.GetString("Primary"));
        Debug.Log("Secondary: " + PlayerPrefs.GetString("Secondary"));
        Debug.Log("SpecialQ: " + PlayerPrefs.GetString("SpecialQ"));
        Debug.Log("SpecialE: " + PlayerPrefs.GetString("SpecialE"));
        Debug.Log("Ultimate: " + PlayerPrefs.GetString("Ultimate"));
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
    void FixedUpdate() //this runs once per physics frame, not in game frames
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
