using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Allows for the ESC functionality in the ability select screen.
/// - Austin
/// </summary>

public class MainMenuInputHelper : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.UIActions ui;

    public Hotbar hotbar;

    void Awake()
    {
        playerInput = new PlayerInput();
        ui = playerInput.UI;

        ui.Cancel.performed += ctx => hotbar.DeselectButtons();

        UIEnable();
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
