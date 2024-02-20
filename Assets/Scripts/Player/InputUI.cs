using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputUI : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.PlayerActions player;
    public PlayerInput.UIActions ui;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.None;

        playerInput = new PlayerInput();
        ui = playerInput.UI;

        UIEnable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
    private void LateUpdate()
    {

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
